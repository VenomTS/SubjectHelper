using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Controls.Notifications;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.AbsenceForm;
using SubjectHelper.Components.EvaluationForm;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.Interfaces.Repositories;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.Models;
using SubjectHelper.Models.Customs;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.ViewModels;

public partial class SubjectViewModel : PageViewModel
{
    private readonly ISubjectRepository _subjectRepo;
    private readonly IEvaluationRepository _evaluationRepo;
    private readonly IAbsenceRepository _absenceRepo;
    private readonly IDialogService _dialogService;
    private readonly IToastService _toastService;

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _code = string.Empty;
    [ObservableProperty] private int _weightedGrade;
    [ObservableProperty] private Color _color;
    
    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];
    public ObservableCollection<AbsenceViewModel> Absences { get; set; } = [];
    public int SubjectId { get; private set; }

    public SubjectViewModel(ISubjectRepository subjectRepo, IEvaluationRepository evaluationRepo, IAbsenceRepository absenceRepo, IDialogService dialogService, IToastService toastService)
    {
        Page = ApplicationPages.Subject;
        
        _subjectRepo = subjectRepo;
        _evaluationRepo = evaluationRepo;
        _absenceRepo = absenceRepo;
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
        Color = ColorToHexConverter.ParseHexString(subject.Color, AlphaComponentPosition.Leading)!.Value;

        var evaluations = await _evaluationRepo.GetEvaluationsBySubjectIdAsync(SubjectId);
        foreach (var evaluation in evaluations)
            Evaluations.Add(CreateEvaluationViewModel(evaluation));

        var absences = await _absenceRepo.GetAbsencesBySubjectIdAsync(SubjectId);
        foreach(var absence in absences)
            Absences.Add(CreateAbsenceViewModel(absence));
    }

    [RelayCommand]
    private async Task RandomlyPickWhichOneToAdd()
    {
        var random = new Random();
        if (random.Next(0, 2) == 0)
            await CreateEvaluation();
        else
            await CreateAbsence();
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
        
        if (evaluation == null)
        {
            _toastService.ShowToast("Evaluation Already Exists", NotificationType.Error);
            return;
        }
        
        Evaluations.Add(CreateEvaluationViewModel(evaluation!));
        _toastService.ShowToast("Evaluation Created", NotificationType.Success);
    }

    [RelayCommand]
    private async Task CreateAbsence()
    {
        var vm = new AbsenceFormViewModel();

        var result = await _dialogService.ShowAbsenceForm("Create Absence", vm);

        if (result != DialogResult.OK) return;

        var date = new DateOnly(vm.Date.Year, vm.Date.Month, vm.Date.Day);

        var absence = await _absenceRepo.AddAbsenceAsync(new Absence
        {
            Type = vm.SelectedAbsenceType,
            Title = vm.Title,
            Week = vm.Week,
            HoursMissed = vm.HoursMissed,
            Date = date,
            SubjectId = SubjectId,
        });

        if (absence == null)
        {
            _toastService.ShowToast("Absence Already Exists", NotificationType.Error);
            return;
        }

        Absences.Add(CreateAbsenceViewModel(absence!));
        _toastService.ShowToast("Absence Created", NotificationType.Success);
    }

    [RelayCommand]
    private async Task EditEvaluation(EvaluationViewModel evaluationVM)
    {
        var maxWeight = await GetMaximumAllowedWeight(evaluationVM.Weight);

        var vm = new EvaluationFormViewModel(evaluationVM.Name, evaluationVM.Weight, evaluationVM.Grade, maxWeight);
        
        var result = await _dialogService.ShowEvaluationForm("Edit Evaluation", vm);
        
        if (result != DialogResult.OK) return;

        var evaluationIndex = Evaluations.IndexOf(evaluationVM);
        
        var newEvaluation = await _evaluationRepo.UpdateEvaluationAsync(evaluationVM.Id, SubjectId, new EvaluationUpdate
        {
            Name = vm.Title,
            Weight = vm.Weight,
            Grade = vm.Grade,
        });

        if (newEvaluation == null)
        {
            _toastService.ShowToast("Evaluation Already Exists", NotificationType.Error);
            return;
        }
        
        Evaluations[evaluationIndex] = CreateEvaluationViewModel(newEvaluation!);
        _toastService.ShowToast("Evaluation Edited", NotificationType.Success);
    }

    [RelayCommand]
    private async Task EditAbsence(AbsenceViewModel absenceVM)
    {
        var vm = new AbsenceFormViewModel(absenceVM.Type, absenceVM.Title, absenceVM.Week, absenceVM.HoursMissed, absenceVM.Date);
        
        var result = await _dialogService.ShowAbsenceForm("Edit Evaluation", vm);
        
        if (result != DialogResult.OK) return;
        
        var absenceIndex = Absences.IndexOf(absenceVM);

        var newAbsence = await _absenceRepo.UpdateAbsenceAsync(absenceVM.AbsenceId, absenceVM.SubjectId, 
            new AbsenceUpdate
            {
                Type = vm.SelectedAbsenceType,
                Title = vm.Title,
                Week = vm.Week,
                HoursMissed = vm.HoursMissed,
                Date = new DateOnly(vm.Date.Year, vm.Date.Month, vm.Date.Day),
            });
        
        if (newAbsence == null)
        {
            _toastService.ShowToast("Absence Already Exists", NotificationType.Error);
            return;
        }

        Absences[absenceIndex] = CreateAbsenceViewModel(newAbsence);
        _toastService.ShowToast("Absence Edited", NotificationType.Success);
    }

    [RelayCommand]
    private async Task DeleteEvaluation(EvaluationViewModel evaluationViewModel)
    {
        await _evaluationRepo.DeleteEvaluationAsync(evaluationViewModel.Id);
        
        Evaluations.Remove(evaluationViewModel);
        
        _toastService.ShowToast("Evaluation Deleted", NotificationType.Success);
    }

    [RelayCommand]
    private async Task DeleteAbsence(AbsenceViewModel absenceViewModel)
    {
        await _absenceRepo.DeleteAbsenceAsync(absenceViewModel.AbsenceId);

        Absences.Remove(absenceViewModel);
        
        _toastService.ShowToast("Absence Deleted", NotificationType.Success);
    }

    private static EvaluationViewModel CreateEvaluationViewModel(Evaluation evaluation)
    {
        return new EvaluationViewModel(evaluation);
    }
    
    private static AbsenceViewModel CreateAbsenceViewModel(Absence absence)
    {
        return new AbsenceViewModel(absence);
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