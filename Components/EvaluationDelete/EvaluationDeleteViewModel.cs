using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SubjectHelper.Components.EvaluationDelete;

public partial class EvaluationDeleteViewModel : AvaloniaObject
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<EvaluationDeleteViewModel, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}