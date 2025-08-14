using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Helper;
using SubjectHelper.Models;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels;

public partial class AbsenceViewModel : ViewModelBase
{
    public int AbsenceId { get; }
    public int SubjectId { get; }

    [ObservableProperty] private AbsenceTypes _type;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _dateString;
    [ObservableProperty] private int _week;
    [ObservableProperty] private int _hoursMissed;

    [ObservableProperty] private Color _textColor;
    [ObservableProperty] private Color _backgroundColor;
    
    [ObservableProperty] private DateOnly _date;

    partial void OnDateChanged(DateOnly value) => DateString = value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    
    // First value in array is background, second is text
    private readonly Dictionary<AbsenceTypes, Color[]> _colorDictionary = new()
    {
        { AbsenceTypes.Lecture, [new Color(255, 255, 235, 238), new Color(255, 211, 47, 47)] },
        { AbsenceTypes.Tutorial, [new Color(255, 255, 243, 224), new Color(255, 230, 81, 0)]},
        { AbsenceTypes.Lab, [new Color(255, 232, 245, 233), new Color(255, 46, 125, 50)]},
    };

    partial void OnTypeChanged(AbsenceTypes value)
    {
        BackgroundColor = _colorDictionary[value][0];
        TextColor = _colorDictionary[value][1];
    }

    public AbsenceViewModel(Absence absence)
    {
        Type = absence.Type;
        Title = absence.Title;
        Week = absence.Week;
        HoursMissed = absence.HoursMissed;
        Date = absence.Date;
        DateString = string.Empty;

        AbsenceId = absence.Id;
        SubjectId = absence.SubjectId;

        OnTypeChanged(Type);
        OnDateChanged(Date);
    }
}