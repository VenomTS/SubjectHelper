using System;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public partial class SMTimeViewModel : ViewModelBase
{
    public int Id { get; set; }

    [ObservableProperty] private DayOfWeek _day;
    [ObservableProperty] private TimeOnly _startTime;
    [ObservableProperty] private TimeOnly _endTime;

    public int SMSectionId { get; set; }
    public int SMSubjectId { get; set; }

    public SMTimeViewModel()
    {
        
    }

    public SMTimeViewModel(SMTime time)
    {
        Id = time.Id;
        SMSectionId = time.SMSectionId;
        SMSubjectId = time.SMSection!.SMSubjectId;
        Day = time.Day;
        StartTime = time.StartTime;
        EndTime = time.EndTime;
    }
}