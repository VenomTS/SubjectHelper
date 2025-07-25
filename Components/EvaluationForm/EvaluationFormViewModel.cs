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
    
    private bool SaveEvaluationCanExecute => 
        !string.IsNullOrWhiteSpace(Title) &&
        Weight > 0 && Weight <= MaximumWeight &&
        Grade is >= 0 and <= 100;

    public EvaluationFormViewModel()
    {
        Title = string.Empty;
        Weight = 1;
        Grade = 0;
    }

    public EvaluationFormViewModel(string name, decimal weight, int grade)
    {
        Title = name;
        Weight = weight;
        Grade = grade;
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