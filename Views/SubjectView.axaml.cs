using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SubjectHelper.ViewModels;
using Ursa.Controls;

namespace SubjectHelper.Views;

public partial class SubjectView : UserControl
{
    private SubjectViewModel? _subjectVM;
    
    public SubjectView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (DataContext is not SubjectViewModel vm) return;
        _subjectVM = vm;
        _subjectVM.ToastManager = new WindowToastManager(TopLevel.GetTopLevel(this)) { MaxItems = 1 };
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _subjectVM?.ToastManager?.Uninstall();
    }
}