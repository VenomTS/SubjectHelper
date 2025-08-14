using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMaker.Forms.Subject;

public partial class SMSubjectFormViewModel : ViewModelBase, IDialogContext
{

    [ObservableProperty] private string _header = string.Empty;

    [ObservableProperty] private string _title = string.Empty;

    [RelayCommand]
    private void Save() => RequestClose?.Invoke(this, DialogResult.OK);
    
    [RelayCommand]
    private void Cancel() => Close();
    
    public void Close() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public event EventHandler<object?>? RequestClose;
}