using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace SubjectHelper.Components.ScheduleMaker.Forms.Subject;

public partial class SMSubjectFormView : UserControl
{
    public SMSubjectFormView()
    {
        InitializeComponent();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        SubjectNameInput.Focus();
    }
}