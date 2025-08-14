using System;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public partial class SMTimeViewModel : ViewModelBase
{
    public int Id { get; set; }

    [ObservableProperty] private DayOfWeek _day;
    [ObservableProperty] private TimeOnly _startTime;
    [ObservableProperty] private TimeOnly _endTime;
}