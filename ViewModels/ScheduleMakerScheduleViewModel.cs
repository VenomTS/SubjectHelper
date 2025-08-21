using System;
using System.Collections.Generic;
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
    private List<SMSubjectViewModel> _subjects = [];

    private List<List<SMSectionViewModel>> _allSchedules = [];

    public Dictionary<DayOfWeek, List<SMSectionViewModel>> CurrentSchedule = new();

    private int _currentPage = 1;
    
    public ScheduleMakerScheduleViewModel(IScheduleMakerRepository scheduleMakerRepo, IScheduleMakingService scheduleMakingService)
    {
        Page = ApplicationPages.ScheduleMakerSchedule;

        InitializeSubjects(scheduleMakerRepo);

        var (possibleSubject, possibleListOfSubjects) = scheduleMakingService.GenerateAllSchedules(_subjects);

        if (possibleSubject != null)
        {
            Console.WriteLine($"Cannot fit subject {possibleSubject.Title}");
            return;
        }

        _allSchedules = possibleListOfSubjects!;

        foreach (var lst in _allSchedules)
        {
            foreach (var section in lst)
            {
                var subject = scheduleMakerRepo.GetSubject(section.SMSubjectId).Result;
                if (subject == null) throw new Exception("Subject is null but has section????");
                Console.WriteLine($"{subject.Title}.{section.SMSectionId}");
            }
            Console.WriteLine("\nNEW LIST");
        }
    }

    private void InitializeSubjects(IScheduleMakerRepository scheduleMakerRepo)
    {
        var allSubjects = scheduleMakerRepo.GetSubjects().Result;
        
        foreach(var subject in allSubjects)
            _subjects.Add(new SMSubjectViewModel(subject));
    }

    [RelayCommand]
    private void GoNext()
    {
        _currentPage += 1;
    }

    [RelayCommand]
    private void GoPrevious()
    {
        _currentPage -= 1;
    }
}