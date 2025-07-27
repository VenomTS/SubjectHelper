using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.ScheduleMakerComponents.SectionForm;
using SubjectHelper.Components.ScheduleMakerComponents.TimeComponent;
using SubjectHelper.Components.ScheduleMakerComponents.TimeForm;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMakerComponents.SectionComponent;

public partial class SectionComponentViewModel : ViewModelBase
{
    private SectionSM _section;

    private readonly ISectionSMRepository _sectionRepo;
    private readonly ITimeSMRepository _timeRepo;
    
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _instructor;

    public ObservableCollection<TimeComponentViewModel> Times { get; set; } = [];
    
    private static readonly DialogOptions DefaultDialogOptions = new()
    {
        CanResize = false,
        IsCloseButtonVisible = false,
        Mode = DialogMode.None,
        StartupLocation = WindowStartupLocation.CenterOwner,
    };

    public SectionComponentViewModel(ISectionSMRepository sectionRepo, ITimeSMRepository timeRepo, SectionSM section)
    {
        _sectionRepo = sectionRepo;
        _timeRepo = timeRepo;
        _section = section;
        _title = _section.Title;
        _instructor = _section.Instructor;

        PopulateTimes();
    }

    [RelayCommand]
    private async Task AddTime()
    {
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(10, 0);
        var timeFormVM = new TimeFormViewModel("Create Timeslot", startTime: startTime, endTime: endTime);

        var result = await Dialog.ShowCustomModal<TimeFormView, TimeFormViewModel, DialogResult>(timeFormVM, null, DefaultDialogOptions);

        if (result != DialogResult.OK) return;
        
        var time = new TimeSM
        {
            Day = timeFormVM.SelectedDayIndex,
            StartTime = timeFormVM.StartTime,
            EndTime = timeFormVM.EndTime,
        };
        
        time = await _timeRepo.AddTimeAsync(time);
        
        var timeVM = new TimeComponentViewModel(_timeRepo, time!);
        timeVM.OnDeletePressed += DeleteTime;
        _section.Times.Add(time!);
        Times.Add(timeVM);
    }

    [RelayCommand]
    private async Task Edit()
    {
        var sectionFormVM = new SectionFormViewModel("Edit Section")
        {
            Title = _section.Title,
            Instructor = "No Instructor",
        };
        
        var result = await Dialog.ShowCustomModal<SectionFormView, SectionFormViewModel, DialogResult>(sectionFormVM, null, DefaultDialogOptions);
        
        if (result != DialogResult.OK) return;

        var section = new SectionSM
        {
            Title = sectionFormVM.Title,
            Instructor = sectionFormVM.Instructor,
        };
        
        section = await _sectionRepo.UpdateSectionAsync(_section.Id, section);
        if (section == null)
            throw new NotImplementedException("Toast that says that Section already exists");
        
        _section = section;
        
        Title = _section.Title;
        Instructor = _section.Instructor;
    }
    
    [RelayCommand]
    private async Task Delete()
    {
        var allTimes = await _timeRepo.GetTimesBySectionIdAsync(_section.Id);
        foreach (var time in allTimes)
            await _timeRepo.DeleteTimeAsync(time.Id);
        await _sectionRepo.DeleteSectionAsync(_section.Id);
    }

    private void PopulateTimes()
    {
        foreach (var time in _section.Times)
        {
            var timeVM = new TimeComponentViewModel(_timeRepo, time);
            timeVM.OnDeletePressed += DeleteTime;
            Times.Add(timeVM);
        }
    }

    private void DeleteTime(object? sender, EventArgs e)
    {
        if (sender is not TimeComponentViewModel timeVM) return;
        Times.Remove(timeVM);
    }
}