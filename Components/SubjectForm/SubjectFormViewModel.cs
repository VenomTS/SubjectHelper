using System;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.SubjectForm;

public partial class SubjectFormViewModel : ViewModelBase, IDialogContext
{
    [ObservableProperty] private string _header = string.Empty;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _title = string.Empty;
    
    [ObservableProperty] private string _code = string.Empty;
    [ObservableProperty] private Color _color = Color.FromArgb(255, 255, 255, 255);

    private bool CanExecuteSave => !string.IsNullOrWhiteSpace(Title);

    [RelayCommand(CanExecute = nameof(CanExecuteSave))]
    private void Save() => RequestClose?.Invoke(this, DialogResult.OK);

    [RelayCommand]
    private void Cancel() => Close();

    public void Close() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public event EventHandler<object?>? RequestClose;
}