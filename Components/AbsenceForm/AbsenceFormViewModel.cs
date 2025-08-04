using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.Helper;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.AbsenceForm;

public partial class AbsenceFormViewModel : ViewModelBase, IDialogContext
{
    private static readonly DateTime Today = DateTime.Now;
    
    [ObservableProperty] private string _header = string.Empty;
    [ObservableProperty] private int _selectedAbsence;
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private int _week = 1;
    [ObservableProperty] private int _hoursMissed = 1;
    [ObservableProperty] private DateTimeOffset _date = new(Today);

    public AbsenceTypes SelectedAbsenceType { get; set; }

    partial void OnSelectedAbsenceChanged(int value)
    {
        SelectedAbsenceType = value switch
        {
            0 => AbsenceTypes.Lecture,
            1 => AbsenceTypes.Tutorial,
            2 => AbsenceTypes.Lab,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public AbsenceFormViewModel()
    {
        
    }

    public AbsenceFormViewModel(AbsenceTypes absenceType, string title, int week, int hoursMissed, DateOnly date)
    {
        SelectedAbsence = absenceType switch
        {
            AbsenceTypes.Lecture => 0,
            AbsenceTypes.Tutorial => 1,
            AbsenceTypes.Lab => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(absenceType), absenceType, null)
        };
        Title = title;
        Week = week;
        HoursMissed = hoursMissed;
        Date = new DateTimeOffset(date, TimeOnly.MinValue, TimeSpan.Zero);
        SelectedAbsenceType = absenceType;
    }

    [RelayCommand]
    private void Save() => RequestClose?.Invoke(this, DialogResult.OK);

    [RelayCommand] 
    private void Cancel() => Close();
    
    public void Close() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public event EventHandler<object?>? RequestClose;
}