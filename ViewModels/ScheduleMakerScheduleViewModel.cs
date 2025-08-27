using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using Avalonia.Rendering;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.ViewModels.Bases;
using SubjectHelper.ViewModels.ScheduleMaker;

namespace SubjectHelper.ViewModels;

public partial class ScheduleMakerScheduleViewModel : PageViewModel
{
    private readonly IScheduleMakerRepository _scheduleMakerRepo;
    
    private readonly List<SMSubjectViewModel> _subjects = [];

    private readonly List<List<SMSectionViewModel>> _allSchedules = [];

    private readonly List<SMDisplayViewModel> _currentSchedule = [];

    private int _currentPage = 1;

    public event EventHandler<ScheduleChangedEventArgs> OnCurrentScheduleChanged;

    private bool CanGoNext => _currentPage < _allSchedules.Count;
    private bool CanGoPrevious => _currentPage > 1;
    
    public ScheduleMakerScheduleViewModel(IScheduleMakerRepository scheduleMakerRepo, IScheduleMakingService scheduleMakingService, IToastService toastService)
    {
        Page = ApplicationPages.ScheduleMakerSchedule;

        _scheduleMakerRepo = scheduleMakerRepo;

        InitializeSubjects(_scheduleMakerRepo);

        var (possibleSubject, possibleListOfSubjects) = scheduleMakingService.GenerateAllSchedules(_subjects);

        if (possibleSubject != null)
        {
            toastService.ShowToast($"Cannot create schedule - Problem: {possibleSubject.Title}", NotificationType.Error);
            return;
        }

        _allSchedules = possibleListOfSubjects!;

        _ = UpdateSchedule();
    }

    public void TriggerScheduleChange()
    {
        OnCurrentScheduleChanged?.Invoke(this, new ScheduleChangedEventArgs
        {
            Schedule = _currentSchedule,
        });
    }

    private async Task UpdateSchedule()
    {
        _currentSchedule.Clear();
        var selectedSchedule = _allSchedules[_currentPage - 1];
        foreach (var section in selectedSchedule)
        {
            var subject = await _scheduleMakerRepo.GetSubject(section.SMSubjectId);
            foreach (var time in section.Times)
            {
                var displayViewModel = new SMDisplayViewModel
                {
                    Day = time.Day,
                    StartTime = time.StartTime,
                    EndTime = time.EndTime,
                    SectionFullName = $"{subject!.Title}.{section.SMSectionId}",
                };
                _currentSchedule.Add(displayViewModel);
            }
        }
        TriggerScheduleChange();
    }

    private void InitializeSubjects(IScheduleMakerRepository scheduleMakerRepo)
    {
        var allSubjects = scheduleMakerRepo.GetSubjects().Result;
        
        Console.WriteLine($"{allSubjects.Count}");
        
        foreach(var subject in allSubjects)
            _subjects.Add(new SMSubjectViewModel(subject));
    }

    [RelayCommand(CanExecute = nameof(CanGoNext))]
    private async Task GoNext()
    {
        _currentPage += 1;
        GoNextCommand.NotifyCanExecuteChanged();
        GoPreviousCommand.NotifyCanExecuteChanged();
        await UpdateSchedule();
    }

    [RelayCommand(CanExecute = nameof(CanGoPrevious))]
    private async Task GoPrevious()
    {
        _currentPage -= 1;
        GoNextCommand.NotifyCanExecuteChanged();
        GoPreviousCommand.NotifyCanExecuteChanged();
        await UpdateSchedule();
    }
}

public class ScheduleChangedEventArgs : EventArgs
{
    public List<SMDisplayViewModel> Schedule { get; set; } = [];
}