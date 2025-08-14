using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SubjectHelper.ViewModels.ScheduleMaker;

namespace SubjectHelper.Components.ScheduleMaker;

public partial class SMSubjectCard : UserControl
{
    public SMSubjectCard()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<SMSubjectCard, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<SMSubjectCard, ICommand>(
        nameof(EditCommand));

    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly StyledProperty<object?> EditParameterProperty = AvaloniaProperty.Register<SMSubjectCard, object?>(
        nameof(EditParameter));

    public object? EditParameter
    {
        get => GetValue(EditParameterProperty);
        set => SetValue(EditParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteCommandProperty = AvaloniaProperty.Register<SMSubjectCard, ICommand>(
        nameof(DeleteCommand));

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly StyledProperty<object?> DeleteParameterProperty = AvaloniaProperty.Register<SMSubjectCard, object?>(
        nameof(DeleteParameter));

    public object? DeleteParameter
    {
        get => GetValue(DeleteParameterProperty);
        set => SetValue(DeleteParameterProperty, value);
    }

    public static readonly StyledProperty<IEnumerable<SMSectionViewModel>> SectionsProperty = AvaloniaProperty.Register<SMSubjectCard, IEnumerable<SMSectionViewModel>>(
        nameof(Sections));

    public IEnumerable<SMSectionViewModel> Sections
    {
        get => GetValue(SectionsProperty);
        set => SetValue(SectionsProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteSectionCommandProperty = AvaloniaProperty.Register<SMSubjectCard, ICommand>(
        nameof(DeleteSectionCommand));

    public ICommand DeleteSectionCommand
    {
        get => GetValue(DeleteSectionCommandProperty);
        set => SetValue(DeleteSectionCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditSectionCommandProperty = AvaloniaProperty.Register<SMSubjectCard, ICommand>(
        nameof(EditSectionCommand));

    public ICommand EditSectionCommand
    {
        get => GetValue(EditSectionCommandProperty);
        set => SetValue(EditSectionCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> DeleteTimeCommandProperty = AvaloniaProperty.Register<SMSubjectCard, ICommand>(
        nameof(DeleteTimeCommand));

    public ICommand DeleteTimeCommand
    {
        get => GetValue(DeleteTimeCommandProperty);
        set => SetValue(DeleteTimeCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> EditTimeCommandProperty = AvaloniaProperty.Register<SMSubjectCard, ICommand>(
        nameof(EditTimeCommand));

    public ICommand EditTimeCommand
    {
        get => GetValue(EditTimeCommandProperty);
        set => SetValue(EditTimeCommandProperty, value);
    }
}