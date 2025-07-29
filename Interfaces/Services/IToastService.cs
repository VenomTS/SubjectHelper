using Avalonia.Controls.Notifications;
using Ursa.Controls;

namespace SubjectHelper.Interfaces.Services;

public interface IToastService
{
    void Install(WindowToastManager? windowToastManager);
    void Uninstall();
    
    void ShowToast(string content, NotificationType type);
}