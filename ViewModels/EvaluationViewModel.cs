using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Models;

namespace SubjectHelper.ViewModels;

public partial class EvaluationViewModel : ViewModelBase
{
    private Evaluation _evaluation;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private decimal _weight;

    [ObservableProperty]
    private int _points;

    public EvaluationViewModel(Evaluation evaluation)
    {
        _evaluation = evaluation;
        _name = evaluation.Name;
        _weight = evaluation.Weight;
        _points = evaluation.Points;
    }
}