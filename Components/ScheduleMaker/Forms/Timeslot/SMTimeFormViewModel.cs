using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMaker.Forms.Timeslot;

public partial class SMTimeFormViewModel : ViewModelBase, IDialogContext
{

    [ObservableProperty] private string _header = string.Empty;

    [ObservableProperty] 
    private int _dayIndex;

    partial void OnDayIndexChanged(int value) => Day = value switch
    {
        0 => DayOfWeek.Monday,
        1 => DayOfWeek.Tuesday,
        2 => DayOfWeek.Wednesday,
        3 => DayOfWeek.Thursday,
        4 => DayOfWeek.Friday,
        5 => DayOfWeek.Saturday,
        _ => DayOfWeek.Sunday, 
    };

    [ObservableProperty] 
    private DayOfWeek _day = DayOfWeek.Monday;

    partial void OnDayChanged(DayOfWeek value) => DayIndex = value switch
    {
        DayOfWeek.Monday => 0,
        DayOfWeek.Tuesday => 1,
        DayOfWeek.Wednesday => 2,
        DayOfWeek.Thursday => 3,
        DayOfWeek.Friday => 4,
        DayOfWeek.Saturday => 5,
        _ => 6,
    };

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeOnly _startTime = new(09, 00);
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeOnly _endTime = new(10, 00);

    private bool CanSave => StartTime < EndTime;

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save() => RequestClose?.Invoke(this, DialogResult.OK);

    [RelayCommand]
    private void Cancel() => Close();
    
    public void Close() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public event EventHandler<object?>? RequestClose;
}