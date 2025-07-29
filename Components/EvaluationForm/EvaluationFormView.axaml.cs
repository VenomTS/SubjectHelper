using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SubjectHelper.Models;

namespace SubjectHelper.Components.EvaluationForm;

public partial class EvaluationFormView : UserControl
{
    public EvaluationFormView()
    {
        InitializeComponent();
    }

    private void EvaluationTitleInput_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        EvaluationWeightInput.Focus();
    }

    private void EvaluationWeightInput_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        EvaluationGradeInput.Focus();
    }

    private void EvaluationGradeInput_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        EvaluationTitleInput.Focus();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        EvaluationTitleInput.Focus();
        if (string.IsNullOrWhiteSpace(EvaluationTitleInput.Text)) return;
        EvaluationTitleInput.SelectAll();
    }
}