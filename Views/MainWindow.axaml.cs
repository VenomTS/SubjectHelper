using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using SubjectHelper.ViewModels;
using Ursa.Controls;

namespace SubjectHelper.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel? _viewModel;

    private const int MaxToasts = 3;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if(DataContext is not MainWindowViewModel vm) return;
        _viewModel = vm;
        _viewModel.InstallToastService(new WindowToastManager(GetTopLevel(this)) { MaxItems = MaxToasts });
    }

    private void Control_OnUnloaded(object? sender, RoutedEventArgs e)
    {
        _viewModel?.UninstallToastService();
    }

    private void SplitPanePanel_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        SplitPane.IsPaneOpen = true;
    }

    private void SplitPanePanel_OnPointerExited(object? sender, PointerEventArgs e)
    {
        SplitPane.IsPaneOpen = false;
    }
}