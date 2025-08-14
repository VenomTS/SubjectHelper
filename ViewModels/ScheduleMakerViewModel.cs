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
    }

    [RelayCommand]
    private void AddSubject()
    {
        
    }

    [RelayCommand]
    private void AddSection(SMSubjectViewModel vm)
    {
        Console.WriteLine($"Adding a section to {vm.Title}");
    }

    [RelayCommand]
    private void AddTime(SMSectionViewModel vm)
    {
        Console.WriteLine($"Adding a time to {vm.Id}");
    }
    
    [RelayCommand]
    private void DeleteSubject(SMSubjectViewModel vm)
    {
        Console.WriteLine($"Deleting subject {vm.Title}");
    }

    [RelayCommand]
    private void DeleteSection(SMSectionViewModel vm)
    {
        Console.WriteLine($"Deleting section {vm.Id}");
    }
    
    [RelayCommand]
    private void DeleteTime(SMTimeViewModel vm)
    {
        Console.WriteLine($"Deleting following Timeslot: {vm.Day} - {vm.StartTime} / {vm.EndTime}");
    }
}