using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SubjectHelper.ViewModels;
using Ursa.Controls;

namespace SubjectHelper.Views;

public partial class SubjectsListView : UserControl
{
    private SubjectsListViewModel? _subjectListVM;
    
    public SubjectsListView()
    {
        InitializeComponent();
    }
    
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (DataContext is not SubjectsListViewModel vm) return;
        _subjectListVM = vm;
        _subjectListVM.ToastManager = new WindowToastManager(TopLevel.GetTopLevel(this)) { MaxItems = 1 };
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _subjectListVM?.ToastManager?.Uninstall();
    }
}