using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.ScheduleMaker.Forms.Subject;
using SubjectHelper.Components.ScheduleMaker.Forms.Timeslot;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker.Updates;
using SubjectHelper.ViewModels.Bases;
using SubjectHelper.ViewModels.ScheduleMaker;
using Ursa.Controls;

namespace SubjectHelper.ViewModels;

public partial class ScheduleMakerViewModel : PageViewModel
{
    private readonly IScheduleMakerRepository _scheduleMakerRepo;
    private readonly IDialogService _dialogService;
    private readonly IToastService _toastService;

    public ObservableCollection<SMSubjectViewModel> Subjects { get; } = [];
    
    public ScheduleMakerViewModel(IScheduleMakerRepository scheduleMakerRepo, IDialogService dialogService, IToastService toastService)
    {
        Page = ApplicationPages.ScheduleMakerSubjects;
        _scheduleMakerRepo = scheduleMakerRepo;
        _dialogService = dialogService;
        _toastService = toastService;

        _ = Initialize();
    }

    private async Task Initialize()
    {
        var databaseSubjects = await _scheduleMakerRepo.GetSubjects();

        foreach (var subject in databaseSubjects)
        {
            Subjects.Add(new SMSubjectViewModel(subject));
        }
    }

    [RelayCommand]
    private async Task AddSubject()
    {
        var vm = new SMSubjectFormViewModel();

        var result = await _dialogService.ShowSMSubjectForm("Add Subject", vm);

        if (result != DialogResult.OK) return;

        var subject = await _scheduleMakerRepo.AddSubject(vm.Title);
        
        Subjects.Add(new SMSubjectViewModel(subject));
        
        _toastService.ShowToast("Subject Added", NotificationType.Success);
    }

    [RelayCommand]
    private async Task AddSection(SMSubjectViewModel vm)
    {
        var section = await _scheduleMakerRepo.AddSection(vm.Id);
        
        vm.Sections.Add(new SMSectionViewModel(section));
        
        _toastService.ShowToast("Section Added", NotificationType.Success);
    }

    [RelayCommand]
    private async Task AddTime(SMSectionViewModel vm)
    {
        var timeVM = new SMTimeFormViewModel();

        var result = await _dialogService.ShowSMTimeForm("Add Time", timeVM);

        if (result != DialogResult.OK) return;

        var time = await _scheduleMakerRepo.AddTime(vm.Id, new SMTime
        {
            Day = timeVM.Day,
            StartTime = timeVM.StartTime,
            EndTime = timeVM.EndTime,
            SMSectionId = vm.Id,
        });

        vm.Times.Add(new SMTimeViewModel(time));
        
        _toastService.ShowToast("Time Added", NotificationType.Success);
    }

    [RelayCommand]
    private async Task EditSubject(SMSubjectViewModel vm)
    {
        var subjectVM = new SMSubjectFormViewModel
        {
            Title = vm.Title,
        };

        var result = await _dialogService.ShowSMSubjectForm("Edit Subject", subjectVM);

        if (result != DialogResult.OK) return;

        var subject = await _scheduleMakerRepo.UpdateSubject(vm.Id, new SMSubjectUpdate { Title = subjectVM.Title });

        var currentIndex = Subjects.IndexOf(vm);

        Subjects[currentIndex] = new SMSubjectViewModel(subject!);
        
        _toastService.ShowToast("Subject Edited", NotificationType.Success);
    }

    [RelayCommand]
    private void EditSection() => _toastService.ShowToast("Not Implemented", NotificationType.Error);

    [RelayCommand]
    private async Task EditTime(SMTimeViewModel vm)
    {
        var timeVM = new SMTimeFormViewModel
        {
            Day = vm.Day,
            StartTime = vm.StartTime,
            EndTime = vm.EndTime,
        };

        var result = await _dialogService.ShowSMTimeForm("Edit Timeslot", timeVM);

        if (result != DialogResult.OK) return;

        var time = await _scheduleMakerRepo.UpdateTime(vm.Id,
            new SMTimeUpdate { Day = timeVM.Day, StartTime = timeVM.StartTime, EndTime = timeVM.EndTime });

        var subject = Subjects.First(x => x.Id == vm.SMSubjectId);
        var section = subject.Sections.First(x => x.Id == vm.SMSectionId);

        var timeIndex = section.Times.IndexOf(vm);

        section.Times[timeIndex] = new SMTimeViewModel(time!);
        
        _toastService.ShowToast("Time Edited", NotificationType.Success);
    }
    
    [RelayCommand]
    private async Task DeleteSubject(SMSubjectViewModel vm)
    {
        await _scheduleMakerRepo.RemoveSubject(vm.Id);

        Subjects.Remove(vm);
        
        _toastService.ShowToast("Subject Removed", NotificationType.Warning);
    }

    [RelayCommand]
    private async Task DeleteSection(SMSectionViewModel vm)
    {
        await _scheduleMakerRepo.RemoveSection(vm.Id);

        var subject = Subjects.First(x => x.Id == vm.SMSubjectId);

        subject.Sections.Remove(vm);
        
        _toastService.ShowToast("Subject Removed", NotificationType.Warning);
    }
    
    [RelayCommand]
    private async Task DeleteTime(SMTimeViewModel vm)
    {
        await _scheduleMakerRepo.RemoveTime(vm.Id);

        var subject = Subjects.First(x => x.Id == vm.SMSubjectId);
        
        var section = subject.Sections.First(x => x.Id == vm.SMSectionId);

        section.Times.Remove(vm);
        
        _toastService.ShowToast("Subject Removed", NotificationType.Warning);
    }
}