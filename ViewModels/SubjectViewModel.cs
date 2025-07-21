using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Models;

namespace SubjectHelper.ViewModels;

public partial class SubjectViewModel : ViewModelBase
{
    private Subject _subject;

    [ObservableProperty]
    private string _name;

    public ObservableCollection<EvaluationViewModel> Evaluations { get; } = [];
    
    public SubjectViewModel(Subject subject)
    {
        _subject = subject;
        _name = subject.Name;
        foreach (var evaluation in subject.Evaluations)
            Evaluations.Add(new EvaluationViewModel(evaluation));
    }
}