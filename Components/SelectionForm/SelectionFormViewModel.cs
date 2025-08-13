using System;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.SelectionForm;

public partial class SelectionFormViewModel : ViewModelBase, IDialogContext
{

    // Yes = Absence
    // No  = Evaluation
    
    [RelayCommand] private void CreateAbsence() => RequestClose?.Invoke(this, DialogResult.Yes);

    [RelayCommand] private void CreateEvaluation() => RequestClose?.Invoke(this, DialogResult.No);

    [RelayCommand] private void Cancel() => Close();
    
    public void Close() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public event EventHandler<object?>? RequestClose;
}