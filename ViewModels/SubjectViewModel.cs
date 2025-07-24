using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.EvaluationForm;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.Models;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.ViewModels;

public partial class SubjectViewModel : PageViewModel
{
    private Subject? _subject;
    
    private readonly ISubjectRepository _subjectRepo;

    [ObservableProperty] private string _name = string.Empty;

    [ObservableProperty] private string _code = string.Empty;

    [ObservableProperty] private int _weightedGrade;
    
    [ObservableProperty] private SolidColorBrush _borderColor;

    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];

    public SubjectViewModel(ISubjectRepository subjectRepo)
    {
        Page = ApplicationPages.Subject;

        var color = RandomColorGenerator.GenerateRandomColor();
        
        _borderColor = new SolidColorBrush(color);
        
        _subjectRepo = subjectRepo;
    }

    public void Initialize(string name)
    {
        var subject = _subjectRepo.GetSubject(name);
        
        _subject = subject ?? throw new Exception("Subject not found");
        
        Name = subject.Name;
        Code = subject.Code;
        foreach (var evaluation in subject.Evaluations)
            Evaluations.Add(new EvaluationViewModel(evaluation));
        
        CalculateWeightedGrade();
    }

    [RelayCommand]
    private async Task OpenCreateEvaluationFormDialog()
    {
        var dialogOptions = new DialogOptions
        {
            StartupLocation = WindowStartupLocation.CenterOwner,
            Mode = DialogMode.None,
            IsCloseButtonVisible = false,
            CanResize = false,
        };
        
        var vm = new EvaluationFormViewModel();

        var result = await Dialog.ShowCustomModal<EvaluationFormView, EvaluationFormViewModel, DialogResult>(vm, null, dialogOptions);

        if (result != DialogResult.OK) return;
        
        var evaluation = CreateEvaluation(vm.Title, vm.Weight, vm.Grade);
        
        if(_subject == null)
            throw new Exception("Subject is undefined");

        await _subjectRepo.AddEvaluation(_subject.Name, evaluation);
        
        Evaluations.Add(new EvaluationViewModel(evaluation));
        
        CalculateWeightedGrade();
    }

    [RelayCommand]
    private async Task OpenEditEvaluationFormDialog(EvaluationViewModel evaluationViewModel)
    {
        var dialogOptions = new DialogOptions
        {
            StartupLocation = WindowStartupLocation.CenterOwner,
            Mode = DialogMode.None,
            IsCloseButtonVisible = false,
            CanResize = false,
        };
        
        var vm = new EvaluationFormViewModel(evaluationViewModel.Name, evaluationViewModel.Weight, evaluationViewModel.Grade);
        
        var result = await Dialog.ShowCustomModal<EvaluationFormView, EvaluationFormViewModel, DialogResult>(vm, null, dialogOptions);
        
        if(result != DialogResult.OK) return;
        
        var evaluationIndex = Evaluations.IndexOf(evaluationViewModel);

        if (evaluationIndex == -1)
            throw new Exception("Evaluation not found although it should be present");
        
        var newEvaluation = CreateEvaluation(vm.Title, vm.Weight, vm.Grade);
        
        if(_subject == null)
            throw new Exception("Subject is undefined");
        
        await _subjectRepo.UpdateEvaluationAt(_subject.Name, evaluationIndex, newEvaluation);
        
        Evaluations[evaluationIndex] = new EvaluationViewModel(newEvaluation);
        
        CalculateWeightedGrade();
    }

    [RelayCommand]
    private void DeleteEvaluation(EvaluationViewModel evaluationViewModel)
    {
        var evaluationIndex = Evaluations.IndexOf(evaluationViewModel);
        
        if(evaluationIndex == -1) 
            throw new Exception("Evaluation not found although it should be present");
        
        _subjectRepo.DeleteEvaluationAt(_subject!.Name, evaluationIndex);
        
        Evaluations.RemoveAt(evaluationIndex);
        
        CalculateWeightedGrade();
    }

    private static Evaluation CreateEvaluation(string name, decimal weight, int grade)
    {
        return new Evaluation
        {
            Name = name.Trim(),
            Weight = weight,
            Grade = grade,
        };
    }

    private void CalculateWeightedGrade()
    {
        if(_subject == null)
            throw new Exception("Subject is undefined");
        var weightedGrade = _subject.Evaluations.Sum(evaluation => evaluation.Grade * (evaluation.Weight / 100m));
        WeightedGrade = (int) Math.Round(weightedGrade);
    }
}