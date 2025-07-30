using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SubjectHelper.Components.SubjectForm;

public partial class SubjectFormView : UserControl
{
    public SubjectFormView()
    {
        InitializeComponent();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        SubjectNameInput.Focus();
    }

    private void SubjectName_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        SubjectCodeInput.Focus();
    }

    private void SubjectCode_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        SubjectColorInput.Focus();
    }

    private void SubjectColorInput_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        SubjectColorInput.IsColorModelVisible = true;
    }
}