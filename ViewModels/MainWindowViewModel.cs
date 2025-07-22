using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Models;

namespace SubjectHelper.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    // [ObservableProperty] private ViewModelBase _currentViewModel;

    public ObservableCollection<SubjectViewModel> Subjects { get; } = [];
    
    [ObservableProperty]
    private SubjectViewModel _selectedSubject;

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
        
        Subjects.Add(new SubjectViewModel(subject));
        
        evaluations =
        [
            CreateEvaluation("Midterm", 20, 100),
            CreateEvaluation("Quiz 1", 5, 88),
            CreateEvaluation("Quiz 2", 5, 100),
            CreateEvaluation("Quiz 3", 5, 96),
            CreateEvaluation("Quiz 4", 5, 96),
            CreateEvaluation("Project", 30, 95),
            CreateEvaluation("Final", 30, 100),
        ];

        subject = new Subject
        {
            Name = "Operations Research I",
            Evaluations = evaluations
        };
        
        Subjects.Add(new SubjectViewModel(subject));
        
        SelectedSubject = Subjects[0];
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