using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SubjectHelper.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void UpdatePaneVisibility(object? sender, RoutedEventArgs e)
    {
        SplitPane.IsPaneOpen = !SplitPane.IsPaneOpen;
    }
}