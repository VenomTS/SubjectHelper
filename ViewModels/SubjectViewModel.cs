using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
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
    private readonly IEvaluationRepository _evaluationRepo;

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _code = string.Empty;
    [ObservableProperty] private int _weightedGrade;
    [ObservableProperty] private SolidColorBrush _borderColor;

    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];
    public int Id { get; set; }
    public WindowToastManager? ToastManager { get; set; }
    
    private static readonly DialogOptions CustomDialogOptions = new()
    {
        StartupLocation = WindowStartupLocation.CenterOwner,
        Mode = DialogMode.None,
        IsCloseButtonVisible = false,
        CanResize = false,
    };

    public SubjectViewModel(ISubjectRepository subjectRepo, IEvaluationRepository evaluationRepo)
    {
        Page = ApplicationPages.Subject;

        var color = RandomColorGenerator.GenerateRandomColor();
        
        _borderColor = new SolidColorBrush(color);
        
        _subjectRepo = subjectRepo;
        _evaluationRepo = evaluationRepo;
    }

    public async Task Initialize(int id)
    {
        var subject = await _subjectRepo.GetSubjectByIdAsync(id);
        
        _subject = subject ?? throw new Exception("Subject not found");
        
        Id = subject.Id;
        Name = subject.Name;
        Code = subject.Code;

        var evaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(subject.Id);
        foreach (var evaluation in evaluations)
            Evaluations.Add(new EvaluationViewModel(evaluation));
        
        await CalculateWeightedGrade();
    }

    [RelayCommand]
    private async Task OpenCreateEvaluationFormDialog()
    {
        var vm = new EvaluationFormViewModel
        {
            MaximumWeight = await GetMaximumAllowedWeight()
        };

        if (vm.MaximumWeight <= 0)
        {
            ToastManager?.Show(ToastCreator.CreateToast("Weight is at 100%", NotificationType.Information));
            return;
        }

        var result = await Dialog.ShowCustomModal<EvaluationFormView, EvaluationFormViewModel, DialogResult>(vm, null, CustomDialogOptions);

        if (result != DialogResult.OK) return;

        if (vm.Weight > vm.MaximumWeight)
        {
            ToastManager?.Show(ToastCreator.CreateToast("Weight exceeded", NotificationType.Error));
            return;
        }

        var evaluation = new Evaluation
        {
            Name = vm.Title,
            Weight = Math.Round(vm.Weight, 2),
            Grade = vm.Grade,
            SubjectId = _subject!.Id,
        };
        
        evaluation = await _evaluationRepo.AddEvaluationAsync(evaluation);
        
        Evaluations.Add(new EvaluationViewModel(evaluation!));
        
        await CalculateWeightedGrade();
        
        ToastManager?.Show(ToastCreator.CreateToast("Evaluation added", NotificationType.Success));
    }

    [RelayCommand]
    private async Task OpenEditEvaluationFormDialog(EvaluationViewModel evaluationViewModel)
    {
        var vm = new EvaluationFormViewModel(evaluationViewModel.Name, evaluationViewModel.Weight, evaluationViewModel.Grade) 
        {
            MaximumWeight = await GetMaximumAllowedWeight(evaluationViewModel.Weight)
        };

        var result = await Dialog.ShowCustomModal<EvaluationFormView, EvaluationFormViewModel, DialogResult>(vm, null, CustomDialogOptions);
        
        if(result != DialogResult.OK) return;
        
        if (vm.Weight > vm.MaximumWeight)
        {
            ToastManager?.Show(ToastCreator.CreateToast("Weight exceeded", NotificationType.Error));
            return;
        }
        
        var evaluationIndex = Evaluations.IndexOf(evaluationViewModel);

        var newEvaluation = new Evaluation
        {
            Name = vm.Title,
            Weight = Math.Round(vm.Weight, 2),
            Grade = vm.Grade,
        };
        
        newEvaluation = await _evaluationRepo.UpdateEvaluationAsync(evaluationViewModel.Id, newEvaluation);
        
        Evaluations[evaluationIndex] = new EvaluationViewModel(newEvaluation!);
        
        await CalculateWeightedGrade();
        
        ToastManager?.Show(ToastCreator.CreateToast("Evaluation edited", NotificationType.Success));
    }

    [RelayCommand]
    private async Task DeleteEvaluation(EvaluationViewModel evaluationViewModel)
    {
        await _evaluationRepo.DeleteEvaluationAsync(evaluationViewModel.Id);
        
        Evaluations.Remove(evaluationViewModel);
        
        await CalculateWeightedGrade();
        
        ToastManager?.Show(ToastCreator.CreateToast("Evaluation deleted", NotificationType.Warning));
    }

    private async Task CalculateWeightedGrade()
    {
        var allEvaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(_subject!.Id);

        decimal weightedGrade = allEvaluations.Sum(evaluation => evaluation.Grade * (evaluation.Weight / 100m));

        WeightedGrade = (int)Math.Round(weightedGrade);
    }

    private async Task<decimal> GetMaximumAllowedWeight(decimal replacingWeight = 0)
    {
        var allEvaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(_subject!.Id);
        
        var currentWeight = allEvaluations.Sum(evaluation => evaluation.Weight);

        return 100m - currentWeight + replacingWeight;
    }
}