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

    [ObservableProperty] private DayOfWeek _day;
    
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