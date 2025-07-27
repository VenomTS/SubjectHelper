using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SubjectHelper.Components.ScheduleMakerComponents.SectionForm;

public partial class SectionFormView : UserControl
{
    public SectionFormView()
    {
        InitializeComponent();
    }

    private void TitleInput_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        InstructorInput.Focus();
    }

    private void InstructorInput_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        TitleInput.Focus();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        TitleInput.Focus();
    }
}