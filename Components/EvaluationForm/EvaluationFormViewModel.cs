using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using SubjectHelper.Models;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.EvaluationForm;

public partial class EvaluationFormViewModel : ViewModelBase, IDialogContext
{
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveEvaluationCommand))]
    private string _title;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveEvaluationCommand))]
    private decimal _weight;
    
    [ObservableProperty] private decimal _maximumWeight;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveEvaluationCommand))]
    private int _grade;

    [ObservableProperty] private string _header = string.Empty;
    
    private bool SaveEvaluationCanExecute => 
        !string.IsNullOrWhiteSpace(Title) &&
        Weight > 0 && Weight <= MaximumWeight &&
        Grade is >= 0 and <= 100;

    public EvaluationFormViewModel(decimal maxWeight = 0)
    {
        Title = string.Empty;
        Weight = 1;
        Grade = 0;
        MaximumWeight = maxWeight;
    }

    public EvaluationFormViewModel(EvaluationFormViewModel oldVM, decimal maxWeight = 0)
    {
        Title = oldVM.Title;
        Weight = oldVM.Weight;
        Grade = oldVM.Grade;
        MaximumWeight = maxWeight;
    }

    [RelayCommand(CanExecute = nameof(SaveEvaluationCanExecute))]
    private void SaveEvaluation() => RequestClose?.Invoke(this, DialogResult.OK);
    
    [RelayCommand]
    private void CancelEvaluation() => RequestClose?.Invoke(this, DialogResult.Cancel);

    public void Close()
    {
        RequestClose?.Invoke(this, DialogResult.Cancel);
    }

    public event EventHandler<object?>? RequestClose;
}