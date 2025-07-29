using System;
using Avalonia.Controls.Notifications;
using SubjectHelper.Interfaces;
using Ursa.Controls;

namespace SubjectHelper.Services;

public class ToastService : IToastService
{
    private WindowToastManager? _toastManager;
    
    private static readonly TimeSpan ToastDuration = TimeSpan.FromSeconds(3);
    
    private const bool ShowClose = true;
    private const bool ShowIcon = true;
    
    public void Install(WindowToastManager? windowToastManager) => _toastManager = windowToastManager;
    public void Uninstall() => _toastManager?.Uninstall();

    public void ShowToast(string content, NotificationType type)
    {
        // Error occured when initializing so no more toasts
        if (_toastManager == null) return;
        
        var toast = CreateToast(content, type);
        _toastManager?.Show(toast);
    }

    private static Toast CreateToast(string content, NotificationType type)
    {
        return new Toast
        {
            Content = content,
            Expiration = ToastDuration,
            ShowClose = ShowClose,
            ShowIcon = ShowIcon,
            Type = type,
        };
    }
}