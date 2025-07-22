using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace SubjectHelper.Components;

public class EvaluationForm : TemplatedControl
{

    // Workaround
    public static readonly StyledProperty<bool> TrueValueProperty = AvaloniaProperty.Register<EvaluationForm, bool>(
        nameof(TrueValue), true);

    public bool TrueValue
    {
        get => GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }
    
    public static readonly StyledProperty<bool> FalseValueProperty = AvaloniaProperty.Register<EvaluationForm, bool>(
        nameof(FalseValue));

    public bool FalseValue
    {
        get => GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<EvaluationForm, string>(
        nameof(Title), defaultBindingMode: BindingMode.TwoWay, defaultValue: "");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<decimal> WeightProperty = AvaloniaProperty.Register<EvaluationForm, decimal>(
        nameof(Weight), defaultBindingMode: BindingMode.TwoWay, defaultValue: 0m);

    public decimal Weight
    {
        get => GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }

    public static readonly StyledProperty<decimal> PointsProperty = AvaloniaProperty.Register<EvaluationForm, decimal>(
        nameof(Points), defaultBindingMode: BindingMode.TwoWay, defaultValue: 0);

    public decimal Points
    {
        get => GetValue(PointsProperty);
        set => SetValue(PointsProperty, value);
    }

    public static readonly StyledProperty<ICommand> SaveCommandProperty = AvaloniaProperty.Register<EvaluationForm, ICommand>(
        nameof(SaveCommand));

    public ICommand SaveCommand
    {
        get => GetValue(SaveCommandProperty);
        set => SetValue(SaveCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand> CancelCommandProperty = AvaloniaProperty.Register<EvaluationForm, ICommand>(
        nameof(CancelCommand));

    public ICommand CancelCommand
    {
        get => GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }
    
}