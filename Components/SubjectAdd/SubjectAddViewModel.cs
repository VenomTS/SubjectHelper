using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.SubjectAdd;

public partial class SubjectAddViewModel : ViewModelBase, IDialogContext
{
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddSubjectCommand))]
    private string _subjectName = string.Empty;
    
    [ObservableProperty] private string _subjectCode = string.Empty;
    
    private bool AddSubjectCanExecute => !string.IsNullOrWhiteSpace(SubjectName);

    [RelayCommand(CanExecute = nameof(AddSubjectCanExecute))]
    private void AddSubject()
    {
        RequestClose?.Invoke(this, DialogResult.OK);
    }

    [RelayCommand]
    private void CloseWindow()
    {
        Close();
    }
    
    public void Close()
    {
        RequestClose?.Invoke(this, DialogResult.Cancel);
    }

    public event EventHandler<object?>? RequestClose;
}