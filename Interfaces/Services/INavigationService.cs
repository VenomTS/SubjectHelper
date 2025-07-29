using SubjectHelper.Helper;
using SubjectHelper.ViewModels;

namespace SubjectHelper.Interfaces.Services;

public interface INavigationService
{
    void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel);
    void NavigateToPage(ApplicationPages page, object? data = null);
}