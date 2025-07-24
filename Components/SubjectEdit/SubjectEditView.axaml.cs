using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace SubjectHelper.Components.SubjectEdit;

public partial class SubjectEditView : UserControl
{
    public SubjectEditView()
    {
        InitializeComponent();
    }
    
    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        SubjectNameInput.Focus();
    }

    private void SubjectName_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        SubjectCodeInput.Focus();
    }
    
    private void SubjectCode_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        SubjectNameInput.Focus();
    }
}