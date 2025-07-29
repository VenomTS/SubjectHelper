using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace SubjectHelper.Components.Cards;

public partial class EvaluationCard : UserControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<EvaluationCard, string>(nameof(Title));
    public static readonly StyledProperty<decimal> WeightProperty = AvaloniaProperty.Register<EvaluationCard, decimal>(nameof(Weight));
    public static readonly StyledProperty<int> GradeProperty = AvaloniaProperty.Register<EvaluationCard, int>(nameof(Grade));
    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<EvaluationCard, ICommand>(nameof(EditCommand));
    public static readonly StyledProperty<object?> EditParameterProperty = AvaloniaProperty.Register<EvaluationCard, object?>(nameof(EditParameter));
    public static readonly StyledProperty<ICommand> DeleteCommandProperty = AvaloniaProperty.Register<EvaluationCard, ICommand>(nameof(DeleteCommand));
    public static readonly StyledProperty<object?> DeleteParameterProperty = AvaloniaProperty.Register<EvaluationCard, object?>(nameof(DeleteParameter));
    
    public EvaluationCard()
    {
        InitializeComponent();
    }
    
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public decimal Weight
    {
        get => GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }
    
    public int Grade
    {
        get => GetValue(GradeProperty);
        set => SetValue(GradeProperty, value);
    }
    
    public ICommand EditCommand
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }
    
    public object? EditParameter
    {
        get => GetValue(EditParameterProperty);
        set => SetValue(EditParameterProperty, value);
    }
    
    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }
    
    public object? DeleteParameter
    {
        get => GetValue(DeleteParameterProperty);
        set => SetValue(DeleteParameterProperty, value);
    }
}