using System.Collections.Generic;
using System.Linq;
using SubjectHelper.Interfaces;
using SubjectHelper.Models;

namespace SubjectHelper.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private List<Subject> _subjects = [];

    public SubjectRepository()
    {
        // Load from database, HEHE
        
        // Seeding, Coping and Seeding
        List<Evaluation> evaluations =
        [
            CreateEvaluation("Midterm", 30, 67),
            CreateEvaluation("Quiz 1", 5, 100),
            CreateEvaluation("Quiz 2", 5, 80),
            CreateEvaluation("Project 1", 10, 100),
            CreateEvaluation("Project 2", 10, 100),
            CreateEvaluation("Final", 40, 100),
        ];

        AddSubject(CreateSubject("Computer Architecture", evaluations));
        
        evaluations =
        [
            CreateEvaluation("Midterm", 30, 80),
            CreateEvaluation("Quiz 1", 5, 100),
            CreateEvaluation("Quiz 2", 5, 100),
            CreateEvaluation("Project 1", 10, 95),
            CreateEvaluation("Project 2", 10, 100),
            CreateEvaluation("Final", 40, 83),
        ];

        AddSubject(CreateSubject("Database Management", evaluations));
        
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

        AddSubject(CreateSubject("Operations Research I", evaluations));
        
        evaluations =
        [
            CreateEvaluation("Midterm I", 30, 96),
            CreateEvaluation("Midterm II", 30, 100),
            CreateEvaluation("Final", 40, 100),
        ];

        AddSubject(CreateSubject("Introduction to Management", evaluations));
        
        evaluations =
        [
            CreateEvaluation("Quiz 1", 10, 99),
            CreateEvaluation("Midterm", 30, 96),
            CreateEvaluation("Quiz 2", 10, 100),
            CreateEvaluation("In-Lab Assignment", 15, 100),
            CreateEvaluation("Final", 35, 96),
        ];

        AddSubject(CreateSubject("Discrete Mathematics II", evaluations));
    }
    
    public IEnumerable<Subject> GetSubjects()
    {
        return  _subjects;
    }

    public Subject? GetSubject(string name)
    {
        throw new System.NotImplementedException();
    }

    public Subject? AddSubject(Subject subject)
    {
        if (_subjects.Any(s => s.Name == subject.Name))
            return null;
        _subjects.Add(subject);
        return subject;
    }

    public Subject? RemoveSubject(Subject subject)
    {
        throw new System.NotImplementedException();
    }

    private Subject CreateSubject(string name, IEnumerable<Evaluation> evaluations)
    {
        return new Subject
        {
            Name = name,
            Evaluations = evaluations.ToList()
        };
    }

    private Evaluation CreateEvaluation(string name, decimal weight, int points)
    {
        return new Evaluation
        {
            Name = name,
            Weight = weight,
            Points = points
        };
    }
}