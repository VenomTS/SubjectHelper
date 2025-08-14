using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;
using SubjectHelper.ViewModels.ScheduleMaker;

namespace SubjectHelper.ViewModels;

public partial class ScheduleMakerViewModel : PageViewModel
{
    private readonly IScheduleMakerRepository _scheduleMakerRepo;

    public ObservableCollection<SMSubjectViewModel> Subjects { get; } = [];
    
    public ScheduleMakerViewModel(IScheduleMakerRepository scheduleMakerRepo)
    {
        Page = ApplicationPages.ScheduleMakerSubjects;
        _scheduleMakerRepo = scheduleMakerRepo;
        
        InitializeTestingData();
    }

    private void InitializeTestingData()
    {
        var math = new SMSubjectViewModel
        {
            Id = 1,
            Title = "Mathematics",
            Sections =
            [
                new SMSectionViewModel
                {
                    Id = 1, Times =
                    [
                        new SMTimeViewModel
                            { Day = DayOfWeek.Monday, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(10, 0) },
                        new SMTimeViewModel
                        {
                            Day = DayOfWeek.Wednesday, StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(12, 0)
                        },
                    ]
                },
                new SMSectionViewModel
                {
                    Id = 2, Times =
                    [
                        new SMTimeViewModel
                            { Day = DayOfWeek.Tuesday, StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(11, 0) }
                    ]
                },
            ]
        };
        
        var physics = new SMSubjectViewModel
        {
            Id = 2,
            Title = "Physics",
            Sections =
            [
                new SMSectionViewModel
                {
                    Id = 3, Times =
                    [
                        new SMTimeViewModel
                            { Day = DayOfWeek.Thursday, StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(14, 0) },
                        new SMTimeViewModel
                        {
                            Day = DayOfWeek.Friday, StartTime = new TimeOnly(15, 0), EndTime = new TimeOnly(16, 0)
                        },
                    ]
                }
            ]
        };
        Subjects.Add(math);
        Subjects.Add(physics);
    }
    
    [RelayCommand]
    private void DeleteSubject()
    {
        Console.WriteLine("Deleting a subject");
    }

    [RelayCommand]
    private void DeleteSection()
    {
        Console.WriteLine("Deleting a section");
    }
    
    [RelayCommand]
    private void DeleteTime(SMTimeViewModel vm)
    {
        Console.WriteLine($"Deleting following Timeslot: {vm.Day} - {vm.StartTime} / {vm.EndTime}");
    }
}