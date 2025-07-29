using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
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
    private readonly ISubjectRepository _subjectRepo;
    private readonly IEvaluationRepository _evaluationRepo;
    private readonly IDialogService _dialogService;
    private readonly IToastService _toastService;

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _code = string.Empty;
    [ObservableProperty] private int _weightedGrade;
    [ObservableProperty] private SolidColorBrush _borderColor;
    
    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];
    public int SubjectId { get; set; }

    public SubjectViewModel(ISubjectRepository subjectRepo, IEvaluationRepository evaluationRepo, IDialogService dialogService, IToastService toastService)
    {
        Page = ApplicationPages.Subject;

        var color = RandomColorGenerator.GenerateRandomColor();
        
        _borderColor = new SolidColorBrush(color);
        
        _subjectRepo = subjectRepo;
        _evaluationRepo = evaluationRepo;
        _dialogService = dialogService;
        _toastService = toastService;
        
        Evaluations.CollectionChanged += EvaluationsUpdated;
    }

    private void EvaluationsUpdated(object? sender, NotifyCollectionChangedEventArgs e) => _ = CalculateWeightedGrade();

    public async Task Initialize(int id)
    {
        var subject = await _subjectRepo.GetSubjectByIdAsync(id);
        if(subject == null)
            throw new Exception("Subject not found");
        
        SubjectId = id;
        Name = subject.Name;
        Code = subject.Code;

        var evaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(SubjectId);
        foreach (var evaluation in evaluations)
            Evaluations.Add(new EvaluationViewModel(evaluation));
    }

    [RelayCommand]
    private async Task CreateEvaluation()
    {
        var maxWeight = await GetMaximumAllowedWeight();
        if (maxWeight <= 0)
        {
            _toastService.ShowToast("Weight is at 100%", NotificationType.Information);
            return;
        }

        var vm = new EvaluationFormViewModel(maxWeight);

        var result = await _dialogService.ShowEvaluationForm("Create Evaluation", vm);

        if (result != DialogResult.OK) return;

        var evaluation = await _evaluationRepo.AddEvaluationAsync(new Evaluation
        {
            Name = vm.Title,
            Weight = vm.Weight,
            Grade = vm.Grade,
            SubjectId = SubjectId,
        });
        
        Evaluations.Add(CreateEvaluationViewModel(evaluation!));
        _toastService.ShowToast("Evaluation Created", NotificationType.Success);
    }

    [RelayCommand]
    private async Task EditEvaluation(EvaluationViewModel evaluationVM)
    {
        var maxWeight = await GetMaximumAllowedWeight(evaluationVM.Weight);
        
        var vm = new EvaluationFormViewModel(new EvaluationFormViewModel
        {
            Title = evaluationVM.Name,
            Weight = evaluationVM.Weight,
            Grade = evaluationVM.Grade,
        }, maxWeight);
        
        var result = await _dialogService.ShowEvaluationForm("Edit Evaluation", vm);
        
        if (result != DialogResult.OK) return;

        var evaluationIndex = Evaluations.IndexOf(evaluationVM);
        
        var newEvaluation = await _evaluationRepo.UpdateEvaluationAsync(evaluationVM.Id, new Evaluation
        {
            Name = vm.Title,
            Weight = vm.Weight,
            Grade = vm.Grade,
        });
        
        Evaluations[evaluationIndex] = CreateEvaluationViewModel(newEvaluation!);
        _toastService.ShowToast("Evaluation Edited", NotificationType.Success);
    }

    [RelayCommand]
    private async Task DeleteEvaluation(EvaluationViewModel evaluationViewModel)
    {
        await _evaluationRepo.DeleteEvaluationAsync(evaluationViewModel.Id);
        
        Evaluations.Remove(evaluationViewModel);
        
        _toastService.ShowToast("Evaluation Deleted", NotificationType.Success);
    }

    private static EvaluationViewModel CreateEvaluationViewModel(Evaluation evaluation)
    {
        return new EvaluationViewModel(evaluation);
    }

    private async Task CalculateWeightedGrade()
    {
        var allEvaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(SubjectId);

        decimal weightedGrade = allEvaluations.Sum(evaluation => evaluation.Grade * (evaluation.Weight / 100m));

        WeightedGrade = (int)Math.Round(weightedGrade);
    }

    private async Task<decimal> GetMaximumAllowedWeight(decimal replacingWeight = 0)
    {
        var allEvaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(SubjectId);
        
        var currentWeight = allEvaluations.Sum(evaluation => evaluation.Weight);

        return 100m - currentWeight + replacingWeight;
    }
}