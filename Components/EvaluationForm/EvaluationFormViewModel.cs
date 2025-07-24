using Avalonia;
using SubjectHelper.Models;

namespace SubjectHelper.Components.EvaluationForm;

public class EvaluationFormViewModel : AvaloniaObject
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<EvaluationFormViewModel, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<decimal> WeightProperty = AvaloniaProperty.Register<EvaluationFormViewModel, decimal>(
        nameof(Weight));

    public decimal Weight
    {
        get => GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }

    public static readonly StyledProperty<int> GradeProperty = AvaloniaProperty.Register<EvaluationFormViewModel, int>(
        nameof(Grade));

    public int Grade
    {
        get => GetValue(GradeProperty);
        set => SetValue(GradeProperty, value);
    }

    public EvaluationFormViewModel()
    {
        Title = string.Empty;
        Weight = 1;
        Grade = 0;
    }

    public EvaluationFormViewModel(string name, decimal weight, int grade)
    {
        Title = name;
        Weight = weight;
        Grade = grade;
    }
}