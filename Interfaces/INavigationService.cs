using SubjectHelper.Helper;
using SubjectHelper.ViewModels;

namespace SubjectHelper.Interfaces;

public interface INavigationService
{
    void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel);
    void NavigateToPage(ApplicationPages page, object? data = null);
}