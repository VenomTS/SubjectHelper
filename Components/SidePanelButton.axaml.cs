using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace SubjectHelper.Components;

public class SidePanelButton : TemplatedControl
{
    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<SidePanelButton, string>(
        nameof(Text));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<string> FontAwesomeIconProperty = AvaloniaProperty.Register<SidePanelButton, string>(
        nameof(FontAwesomeIcon));

    public string FontAwesomeIcon
    {
        get => GetValue(FontAwesomeIconProperty);
        set => SetValue(FontAwesomeIconProperty, value);
    }

    public static readonly StyledProperty<bool> IsTextVisibleProperty = AvaloniaProperty.Register<SidePanelButton, bool>(
        nameof(IsTextVisible));

    public bool IsTextVisible
    {
        get => GetValue(IsTextVisibleProperty);
        set => SetValue(IsTextVisibleProperty, value);
    }

    public static readonly StyledProperty<ICommand> ClickCommandProperty = AvaloniaProperty.Register<SidePanelButton, ICommand>(
        nameof(ClickCommand));

    public ICommand ClickCommand
    {
        get => GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }
}