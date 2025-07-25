using System;
using Avalonia.Controls.Notifications;
using Ursa.Controls;

namespace SubjectHelper.Helper;

public class ToastCreator
{
    public static Toast CreateToast(string content, NotificationType type)
    {
        return new Toast
        {
            Content = content,
            Expiration = TimeSpan.FromSeconds(3),
            ShowClose = false,
            ShowIcon = true,
            Type = type,
        };
    }
}