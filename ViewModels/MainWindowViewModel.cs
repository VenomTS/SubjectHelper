using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    
    [ObservableProperty] private PageViewModel? _currentViewModel;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.SetMainWindowViewModel(this);
        GoToSubjects();
    }

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