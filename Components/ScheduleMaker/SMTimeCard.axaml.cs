using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SubjectHelper.Components.ScheduleMaker;

public partial class SMTimeCard : UserControl
{
    public SMTimeCard()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<DayOfWeek> DayProperty = AvaloniaProperty.Register<SMTimeCard, DayOfWeek>(
        nameof(Day));

    public DayOfWeek Day
    {
        get => GetValue(DayProperty);
        set => SetValue(DayProperty, value);
    }

    public static readonly StyledProperty<TimeOnly> StartTimeProperty = AvaloniaProperty.Register<SMTimeCard, TimeOnly>(
        nameof(StartTime));

    public TimeOnly StartTime
    {
        get => GetValue(StartTimeProperty);
        set => SetValue(StartTimeProperty, value);
    }

    public static readonly StyledProperty<TimeOnly> EndTimeProperty = AvaloniaProperty.Register<SMTimeCard, TimeOnly>(
        nameof(EndTime));

    public TimeOnly EndTime
    {
        get => GetValue(EndTimeProperty);
        set => SetValue(EndTimeProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<SMTimeCard, ICommand>(
        nameof(EditCommand));

    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly StyledProperty<object?> EditParameterProperty = AvaloniaProperty.Register<SMTimeCard, object?>(
        nameof(EditParameter));

    public object? EditParameter
    {
        get => GetValue(EditParameterProperty);
        set => SetValue(EditParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteCommandProperty = AvaloniaProperty.Register<SMTimeCard, ICommand>(
        nameof(DeleteCommand));

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly StyledProperty<object?> DeleteParameterProperty = AvaloniaProperty.Register<SMTimeCard, object?>(
        nameof(DeleteParameter));

    public object? DeleteParameter
    {
        get => GetValue(DeleteParameterProperty);
        set => SetValue(DeleteParameterProperty, value);
    }
}