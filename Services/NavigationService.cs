using SubjectHelper.Factories;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.ViewModels;

namespace SubjectHelper.Services;

public class NavigationService : INavigationService
{
    private readonly PageFactory _factory;
    
    private MainWindowViewModel _mainWindowViewModel;

    public NavigationService(PageFactory factory)
    {
        _factory = factory;
    }

    public void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
    }
    
    public void NavigateToPage(ApplicationPages page, object? data = null)
    {
        _mainWindowViewModel.CurrentViewModel = _factory.GetPageViewModel(page, data);
    }
}