using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.ScheduleMakerComponents.TimeForm;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMakerComponents.TimeComponent;

public partial class TimeComponentViewModel : ViewModelBase
{
    private TimeSM _time;
    private readonly ITimeSMRepository _timeRepo;
    
    [ObservableProperty] private int _day;
    [ObservableProperty] private TimeOnly _startTime;
    [ObservableProperty] private TimeOnly _endTime;

    public WindowToastManager? ToastManager { get; set; }

    public event EventHandler? OnDeletePressed;

    private static readonly DialogOptions DefaultDialogOptions = new()
    {
        CanResize = false,
        IsCloseButtonVisible = false,
        Mode = DialogMode.None,
        StartupLocation = WindowStartupLocation.CenterOwner,
    };

    public TimeComponentViewModel(ITimeSMRepository timeRepo, TimeSM time)
    {
        _timeRepo = timeRepo;
        _time = time;
        _day = _time.Day;
        _startTime = _time.StartTime;
        _endTime = _time.EndTime;
    }

    [RelayCommand]
    private void Delete()
    {
        _timeRepo.DeleteTimeAsync(_time.Id);
        OnDeletePressed?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private async Task Edit()
    {
        var vm = new TimeFormViewModel("Edit Timeslot", Day, StartTime, EndTime);
        
        var result = await Dialog.ShowCustomModal<TimeFormView, TimeFormViewModel, DialogResult>(vm, null, DefaultDialogOptions);

        if (result != DialogResult.OK) return;

        Day = vm.SelectedDayIndex;
        StartTime = vm.StartTime;
        EndTime = vm.EndTime;

        _time = (await _timeRepo.UpdateTimeAsync(_time.Id, _time))!;
        
        ToastManager?.Show(ToastCreator.CreateToast("Timeslot Changed", NotificationType.Success));
    }
}