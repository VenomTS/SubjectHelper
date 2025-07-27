using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMakerComponents.TimeForm;

public partial class TimeFormViewModel : ViewModelBase, IDialogContext
{
    [ObservableProperty] private string _header;
    [ObservableProperty] private int _selectedDayIndex;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeOnly _startTime;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeOnly _endTime;

    private bool CanExecuteSave => StartTime < EndTime;

    public TimeFormViewModel(string header, int selectedDayIndex = 0, TimeOnly startTime = default, TimeOnly endTime = default)
    {
        _header = header;
        _selectedDayIndex = selectedDayIndex;
        _startTime = startTime;
        _endTime = endTime;
    }

    [RelayCommand(CanExecute = nameof(CanExecuteSave))]
    private void Save() => RequestClose?.Invoke(this, DialogResult.OK);

    [RelayCommand]
    private void Cancel() => Close();
    
    public void Close() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public event EventHandler<object?>? RequestClose;
}