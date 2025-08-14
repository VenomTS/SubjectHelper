using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SubjectHelper.ViewModels.ScheduleMaker;

namespace SubjectHelper.Components.ScheduleMaker;

public partial class SMSectionCard : UserControl
{
    public SMSectionCard()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<int> SectionIdProperty = AvaloniaProperty.Register<SMSectionCard, int>(
        nameof(SectionId));

    public int SectionId
    {
        get => GetValue(SectionIdProperty);
        set => SetValue(SectionIdProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteCommandProperty = AvaloniaProperty.Register<SMSectionCard, ICommand>(
        nameof(DeleteCommand));

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly StyledProperty<object?> DeleteParameterProperty = AvaloniaProperty.Register<SMSectionCard, object?>(
        nameof(DeleteParameter));

    public object? DeleteParameter
    {
        get => GetValue(DeleteParameterProperty);
        set => SetValue(DeleteParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<SMSectionCard, ICommand>(
        nameof(EditCommand));

    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly StyledProperty<object?> EditParameterProperty = AvaloniaProperty.Register<SMSectionCard, object?>(
        nameof(EditParameter));

    public object? EditParameter
    {
        get => GetValue(EditParameterProperty);
        set => SetValue(EditParameterProperty, value);
    }

    public static readonly StyledProperty<IEnumerable<SMTimeViewModel>> TimeSlotsProperty = AvaloniaProperty.Register<SMSectionCard, IEnumerable<SMTimeViewModel>>(
        nameof(TimeSlots));

    public IEnumerable<SMTimeViewModel> TimeSlots
    {
        get => GetValue(TimeSlotsProperty);
        set => SetValue(TimeSlotsProperty, value);
    }

    public static readonly StyledProperty<ICommand> AddTimeCommandProperty = AvaloniaProperty.Register<SMSectionCard, ICommand>(
        nameof(AddTimeCommand));

    public ICommand AddTimeCommand
    {
        get => GetValue(AddTimeCommandProperty);
        set => SetValue(AddTimeCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditTimeCommandProperty = AvaloniaProperty.Register<SMSectionCard, ICommand>(
        nameof(EditTimeCommand));

    public ICommand EditTimeCommand
    {
        get => GetValue(EditTimeCommandProperty);
        set => SetValue(EditTimeCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteTimeCommandProperty = AvaloniaProperty.Register<SMSectionCard, ICommand>(
        nameof(DeleteTimeCommand));

    public ICommand DeleteTimeCommand
    {
        get => GetValue(DeleteTimeCommandProperty);
        set => SetValue(DeleteTimeCommandProperty, value);
    }
}