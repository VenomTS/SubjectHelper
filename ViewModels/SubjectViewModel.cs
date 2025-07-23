using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Models;

namespace SubjectHelper.ViewModels;

public partial class SubjectViewModel : ViewModelBase
{
    private readonly Subject _subject;

    [ObservableProperty] private bool _isEvaluationFormDialogOpen;

    [ObservableProperty]
    private string _name;

    [ObservableProperty] private string _newEvaluationTitle = string.Empty;
    [ObservableProperty] private decimal _newEvaluationWeight = 1;
    [ObservableProperty] private int _newEvaluationPoints;

    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];

    public SubjectViewModel(Subject subject)
    {
        _subject = subject;
        _name = subject.Name;
        foreach (var evaluation in subject.Evaluations)
            Evaluations.Add(new EvaluationViewModel(evaluation));
    }

    [RelayCommand]
    private void OpenEvaluationFormDialog()
    {
        NewEvaluationTitle = string.Empty;
        NewEvaluationWeight = 1;
        NewEvaluationPoints = 0;
        IsEvaluationFormDialogOpen = true;
    }

    [RelayCommand]
    private void CloseEvaluationFormDialog(bool isSaveChanges)
    {
        IsEvaluationFormDialogOpen = false;
        if (!isSaveChanges) return;
        
        var evaluation = new Evaluation
        {
            Name = NewEvaluationTitle,
            Weight = NewEvaluationWeight,
            Points = NewEvaluationPoints
        };
        
        _subject.Evaluations.Add(evaluation);
        Evaluations.Add(new EvaluationViewModel(evaluation));
    }
}