using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace SubjectHelper.Components.Cards;

public partial class SubjectCard : UserControl
{
    public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<SubjectCard, Color>(nameof(Color));
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<SubjectCard, string>(nameof(Title));
    public static readonly StyledProperty<string> CodeProperty = AvaloniaProperty.Register<SubjectCard, string>(nameof(Code));
    public static readonly StyledProperty<ICommand> ViewCommandProperty = AvaloniaProperty.Register<SubjectCard, ICommand>(nameof(ViewCommand));
    public static readonly StyledProperty<object?> ViewParameterProperty = AvaloniaProperty.Register<SubjectCard, object?>(nameof(ViewParameter));
    public static readonly StyledProperty<ICommand> EditCommandProperty = AvaloniaProperty.Register<SubjectCard, ICommand>(nameof(EditCommand));
    public static readonly StyledProperty<object?> EditParameterProperty = AvaloniaProperty.Register<SubjectCard, object?>(nameof(EditParameter));
    public static readonly StyledProperty<ICommand> DeleteCommandProperty = AvaloniaProperty.Register<SubjectCard, ICommand>(nameof(DeleteCommand));
    public static readonly StyledProperty<object?> DeleteParameterProperty = AvaloniaProperty.Register<SubjectCard, object?>(nameof(DeleteParameter));

    public SubjectCard()
    {
        InitializeComponent();
    }

    public Color Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public string Code
    {
        get => GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }
    
    public ICommand ViewCommand
    {
        get => GetValue(ViewCommandProperty);
        set => SetValue(ViewCommandProperty, value);
    }
    
    public object? ViewParameter
    {
        get => GetValue(ViewParameterProperty);
        set => SetValue(ViewParameterProperty, value);
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