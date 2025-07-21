using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace SubjectHelper.Components;

public class EvaluationCard : TemplatedControl
{
    
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<EvaluationCard, string>(
        nameof(Title), "Unknown Evaluation");

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
        defaultValue: 0,
        coerce:OnGradeChanged);

    private static int OnGradeChanged(AvaloniaObject arg1, int arg2)
    {
        var control = (EvaluationCard) arg1;
        control.LetterGrade = GradeToLetterGrade(arg2);
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

    private static string GradeToLetterGrade(int grade)
    {
        return grade switch
        {
            >= 95 => "A",
            >= 85 => "A-",
            >= 80 => "B+",
            >= 75 => "B",
            >= 70 => "B-",
            >= 65 => "C+",
            >= 55 => "C",
            >= 45 => "E",
            _ => "F"
        };
    }
    
}