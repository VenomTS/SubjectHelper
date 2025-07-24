using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
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
    private Subject _subject;
    
    private readonly ISubjectRepository _subjectRepo;

    [ObservableProperty] private string _name;

    [ObservableProperty] private string _code;

    [ObservableProperty] private int _weightedGrade;

    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];

    public SubjectViewModel(ISubjectRepository subjectRepo)
    {
        Page = ApplicationPages.Subject;
        
        _subjectRepo = subjectRepo;
    }

    public void Initialize(string name)
    {
        var subject = _subjectRepo.GetSubject(name);
        if(subject == null)
            throw new Exception("Subject not found");
            
        _subject = subject;
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
            Title = "Add Evaluation",
            StartupLocation = WindowStartupLocation.CenterOwner,
            Mode = DialogMode.None,
            Button = DialogButton.OKCancel,
            CanResize = false,
        };

        var vm = new EvaluationFormViewModel();
        
        // Should probably give him owner too
        var result = await Dialog.ShowModal<EvaluationFormView, EvaluationFormViewModel>(vm, options: dialogOptions);

        if (result != DialogResult.OK) return;
        
        var evaluation = CreateEvaluation(vm.Title, vm.Weight, vm.Grade);

        _subject.Evaluations.Add(evaluation);
        
        Evaluations.Add(new EvaluationViewModel(evaluation));
        
        CalculateWeightedGrade();
    }

    [RelayCommand]
    private async Task OpenEditEvaluationFormDialog(EvaluationViewModel evaluationViewModel)
    {
        var dialogOptions = new DialogOptions
        {
            Title = "Edit Evaluation",
            StartupLocation = WindowStartupLocation.CenterOwner,
            Mode = DialogMode.None,
            Button = DialogButton.OKCancel,
            CanResize = false,
        };
        
        var vm = new EvaluationFormViewModel(evaluationViewModel.Name, evaluationViewModel.Weight, evaluationViewModel.Grade);
        
        var result = await Dialog.ShowModal<EvaluationFormView, EvaluationFormViewModel>(vm, options: dialogOptions);
        
        if(result != DialogResult.OK) return;
        
        var evaluationIndex = Evaluations.IndexOf(evaluationViewModel);

        if (evaluationIndex == -1)
            throw new Exception("Evaluation not found although it should be present");
        
        var newEvaluation = CreateEvaluation(vm.Title, vm.Weight, vm.Grade);
        
        _subject.Evaluations[evaluationIndex] = newEvaluation;
        
        Evaluations[evaluationIndex] = new EvaluationViewModel(newEvaluation);
        
        CalculateWeightedGrade();
    }

    [RelayCommand]
    private void DeleteEvaluation(EvaluationViewModel evaluationViewModel)
    {
        var evaluationIndex = Evaluations.IndexOf(evaluationViewModel);
        
        if(evaluationIndex == -1) 
            throw new Exception("Evaluation not found although it should be present");
        
        _subject.Evaluations.RemoveAt(evaluationIndex);
        
        Evaluations.RemoveAt(evaluationIndex);
        
        CalculateWeightedGrade();
    }

    private Evaluation CreateEvaluation(string name, decimal weight, int grade)
    {
        return new Evaluation
        {
            Name = name,
            Weight = weight,
            Grade = grade,
        };
    }

    private void CalculateWeightedGrade()
    {
        decimal weightedGrade = _subject.Evaluations.Sum(evaluation => evaluation.Grade * (evaluation.Weight / 100m));
        WeightedGrade = (int) Math.Round(weightedGrade);
    }
}