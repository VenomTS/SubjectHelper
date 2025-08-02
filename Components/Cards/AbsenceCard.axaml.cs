using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using SubjectHelper.Helper;

namespace SubjectHelper.Components.Cards;

public partial class AbsenceCard : UserControl
{
    public AbsenceCard()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<Color> BackgroundColorProperty = AvaloniaProperty.Register<AbsenceCard, Color>(nameof(BackgroundColor));

    public Color BackgroundColor
    {
        get => GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public static readonly StyledProperty<Color> TextColorProperty = AvaloniaProperty.Register<AbsenceCard, Color>(
        nameof(TextColor));

    public Color TextColor
    {
        get => GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<AbsenceCard, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<DateOnly> DateProperty = AvaloniaProperty.Register<AbsenceCard, DateOnly>(
        nameof(Date));

    public DateOnly Date
    {
        get => GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public static readonly StyledProperty<int> HoursMissedProperty = AvaloniaProperty.Register<AbsenceCard, int>(
        nameof(HoursMissed));

    public int HoursMissed
    {
        get => GetValue(HoursMissedProperty);
        set => SetValue(HoursMissedProperty, value);
    }

    public static readonly StyledProperty<AbsenceTypes> AbsenceTypeProperty = AvaloniaProperty.Register<AbsenceCard, AbsenceTypes>(
        nameof(AbsenceType));

    public AbsenceTypes AbsenceType
    {
        get => GetValue(AbsenceTypeProperty);
        set => SetValue(AbsenceTypeProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<AbsenceCard, ICommand>(
        nameof(EditCommand));

    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly StyledProperty<object?> EditParameterProperty = AvaloniaProperty.Register<AbsenceCard, object?>(
        nameof(EditParameter));

    public object? EditParameter
    {
        get => GetValue(EditParameterProperty);
        set => SetValue(EditParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteCommandProperty = AvaloniaProperty.Register<AbsenceCard, ICommand>(
        nameof(DeleteCommand));

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly StyledProperty<object?> DeleteParameterProperty = AvaloniaProperty.Register<AbsenceCard, object?>(
        nameof(DeleteParameter));

    public object? DeleteParameter
    {
        get => GetValue(DeleteParameterProperty);
        set => SetValue(DeleteParameterProperty, value);
    }

    public static readonly StyledProperty<int> WeekProperty = AvaloniaProperty.Register<AbsenceCard, int>(
        nameof(Week));

    public int Week
    {
        get => GetValue(WeekProperty);
        set => SetValue(WeekProperty, value);
    }
}