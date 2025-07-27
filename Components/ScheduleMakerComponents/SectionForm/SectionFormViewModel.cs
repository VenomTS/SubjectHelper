using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMakerComponents.SectionForm;

public partial class SectionFormViewModel : ViewModelBase, IDialogContext
{
    [ObservableProperty] private string _header;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _title = string.Empty;
    
    [ObservableProperty] private string _instructor = string.Empty;
    
    private bool CanExecuteSave => !string.IsNullOrWhiteSpace(Instructor);

    public SectionFormViewModel(string header)
    {
        _header = header;
    }

    [RelayCommand(CanExecute = nameof(CanExecuteSave))]
    private void Save() => RequestClose?.Invoke(this, DialogResult.OK);

    [RelayCommand]
    private void Cancel() => Close();

    public void Close()
    {
        RequestClose?.Invoke(this, DialogResult.Cancel);
    }

    public event EventHandler<object?>? RequestClose;
}