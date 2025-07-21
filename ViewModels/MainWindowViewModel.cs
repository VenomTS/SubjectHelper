using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Models;

namespace SubjectHelper.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    [ObservableProperty] private ViewModelBase _currentViewModel;

    public MainWindowViewModel()
    {
        List<Evaluation> evaluations =
        [
            CreateEvaluation("Midterm", 30, 67),
            CreateEvaluation("Quiz 1", 5, 100),
            CreateEvaluation("Quiz 2", 5, 80),
            CreateEvaluation("Project 1", 10, 100),
            CreateEvaluation("Project 2", 10, 100),
            CreateEvaluation("Final", 40, 100),
        ];

        var subject = new Subject
        {
            Name = "Computer Architecture",
            Evaluations = evaluations
        };
        
        _currentViewModel = new SubjectViewModel(subject);
    }

    private Evaluation CreateEvaluation(string name, decimal weight, int points)
    {
        return new Evaluation
        {
            Name = name,
            Points = points,
            Weight = weight
        };
    }

}