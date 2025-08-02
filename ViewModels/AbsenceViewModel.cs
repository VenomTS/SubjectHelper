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
    public int AbsenceId { get; set; }
    public int SubjectId { get; set; }

    [ObservableProperty] private AbsenceTypes _type;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _date;
    [ObservableProperty] private int _week;
    [ObservableProperty] private int _hoursMissed;

    [ObservableProperty] private Color _textColor;
    [ObservableProperty] private Color _backgroundColor;
    
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
        _type = absence.Type;
        _title = absence.Title;
        _week = absence.Week;
        _hoursMissed = absence.HoursMissed;

        var date = absence.Date;
        _date = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        AbsenceId = absence.Id;
        SubjectId = absence.SubjectId;

        OnTypeChanged(_type);
    }
}