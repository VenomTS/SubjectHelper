using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using SubjectHelper.Helper;

namespace SubjectHelper.Components;

public class EvaluationCard : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<EvaluationCard, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<decimal> WeightProperty = AvaloniaProperty.Register<EvaluationCard, decimal>(
        nameof(Weight));

    public decimal Weight
    {
        get => GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }

    public static readonly StyledProperty<int> GradeProperty = AvaloniaProperty.Register<EvaluationCard, int>(
        nameof(Grade),
        coerce:OnGradeChanged);

    private static int OnGradeChanged(AvaloniaObject arg1, int arg2)
    {
        var control = (EvaluationCard) arg1;
        control.LetterGrade = GradeManipulator.GetGrade(arg2);
        return arg2;
    }

    public int Grade
    {
        get => GetValue(GradeProperty);
        set => SetValue(GradeProperty, value);
    }

    private string _letterGrade = "F";

    public static readonly DirectProperty<EvaluationCard, string> LetterGradeProperty = AvaloniaProperty.RegisterDirect<EvaluationCard, string>(
        nameof(LetterGrade), o => o.LetterGrade, (o, v) => o.LetterGrade = v);

    public string LetterGrade
    {
        get => _letterGrade;
        set => SetAndRaise(LetterGradeProperty, ref _letterGrade, value);
    }

    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<EvaluationCard, ICommand>(
        nameof(EditCommand));

    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly StyledProperty<object?> EditCommandParameterProperty = AvaloniaProperty.Register<EvaluationCard, object?>(
        nameof(EditCommandParameter));

    public object? EditCommandParameter
    {
        get => GetValue(EditCommandParameterProperty);
        set => SetValue(EditCommandParameterProperty, value);
    }

    public static readonly StyledProperty<ICommand> ConfirmDeleteCommandProperty = AvaloniaProperty.Register<EvaluationCard, ICommand>(
        nameof(ConfirmDeleteCommand));

    public ICommand ConfirmDeleteCommand
    {
        get => GetValue(ConfirmDeleteCommandProperty);
        set => SetValue(ConfirmDeleteCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> CancelDeleteCommandProperty = AvaloniaProperty.Register<EvaluationCard, ICommand>(
        nameof(CancelDeleteCommand));

    public ICommand CancelDeleteCommand
    {
        get => GetValue(CancelDeleteCommandProperty);
        set => SetValue(CancelDeleteCommandProperty, value);
    }

    public static readonly StyledProperty<object?> ConfirmDeleteCommandParameterProperty = AvaloniaProperty.Register<EvaluationCard, object?>(
        nameof(ConfirmDeleteCommandParameter));

    public object? ConfirmDeleteCommandParameter
    {
        get => GetValue(ConfirmDeleteCommandParameterProperty);
        set => SetValue(ConfirmDeleteCommandParameterProperty, value);
    }

    public static readonly StyledProperty<object?> CancelDeleteCommandParameterProperty = AvaloniaProperty.Register<EvaluationCard, object?>(
        nameof(CancelDeleteCommandParameter));

    public object? CancelDeleteCommandParameter
    {
        get => GetValue(CancelDeleteCommandParameterProperty);
        set => SetValue(CancelDeleteCommandParameterProperty, value);
    }
}