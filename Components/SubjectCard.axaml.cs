using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace SubjectHelper.Components;

public class SubjectCard : TemplatedControl
{

    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<SubjectCard, ICommand>(
        nameof(EditCommand));

    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly StyledProperty<object?> EditCommandParameterProperty = AvaloniaProperty.Register<SubjectCard, object?>(
        nameof(EditCommandParameter));

    public object? EditCommandParameter
    {
        get => GetValue(EditCommandParameterProperty);
        set => SetValue(EditCommandParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> ViewCommandProperty = AvaloniaProperty.Register<SubjectCard, ICommand>(
        nameof(ViewCommand));

    public ICommand ViewCommand
    {
        get => GetValue(ViewCommandProperty);
        set => SetValue(ViewCommandProperty, value);
    }

    public static readonly StyledProperty<object?> ViewCommandParameterProperty = AvaloniaProperty.Register<SubjectCard, object?>(
        nameof(ViewCommandParameter));

    public object? ViewCommandParameter
    {
        get => GetValue(ViewCommandParameterProperty);
        set => SetValue(ViewCommandParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> ConfirmDeleteCommandProperty = AvaloniaProperty.Register<SubjectCard, ICommand>(
        nameof(ConfirmDeleteCommand));

    public ICommand ConfirmDeleteCommand
    {
        get => GetValue(ConfirmDeleteCommandProperty);
        set => SetValue(ConfirmDeleteCommandProperty, value);
    }

    public static readonly StyledProperty<object?> ConfirmDeleteCommandParameterProperty = AvaloniaProperty.Register<SubjectCard, object?>(
        nameof(ConfirmDeleteCommandParameter));

    public object? ConfirmDeleteCommandParameter
    {
        get => GetValue(ConfirmDeleteCommandParameterProperty);
        set => SetValue(ConfirmDeleteCommandParameterProperty, value);
    }

    public static readonly StyledProperty<SolidColorBrush> BorderColorProperty = AvaloniaProperty.Register<SubjectCard, SolidColorBrush>(
        nameof(BorderColor));

    public SolidColorBrush BorderColor
    {
        get => GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<SubjectCard, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<string> CodeProperty = AvaloniaProperty.Register<SubjectCard, string>(
        nameof(Code));

    public string Code
    {
        get => GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }
    
}