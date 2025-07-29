using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IToastService _toastService;
    
    [ObservableProperty] private PageViewModel? _currentViewModel;

    public MainWindowViewModel(INavigationService navigationService, IToastService toastService)
    {
        _navigationService = navigationService;
        _navigationService.SetMainWindowViewModel(this);
        
        _toastService = toastService;
        
        GoToSubjects();
    }

    public void InstallToastService(WindowToastManager windowToastManager) => _toastService.Install(windowToastManager);

    public void UninstallToastService() => _toastService.Uninstall();

    [RelayCommand]
    private void GoToSubjects()
    {
        _navigationService.NavigateToPage(ApplicationPages.Subjects);
    }

    [RelayCommand]
    private void GoToScheduleMaker()
    {
        _navigationService.NavigateToPage(ApplicationPages.ScheduleMakerSubjects);
    }
}