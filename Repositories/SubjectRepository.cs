using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.Models;

namespace SubjectHelper.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly List<Subject> _subjects = [];

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

        AddSubject(CreateSubject("Computer Architecture", "CS304", evaluations));
        
        evaluations =
        [
            CreateEvaluation("Midterm", 30, 80),
            CreateEvaluation("Quiz 1", 5, 100),
            CreateEvaluation("Quiz 2", 5, 100),
            CreateEvaluation("Project 1", 10, 95),
            CreateEvaluation("Project 2", 10, 100),
            CreateEvaluation("Final", 40, 83),
        ];

        AddSubject(CreateSubject("Database Management", "CS306", evaluations));
        
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

        AddSubject(CreateSubject("Operations Research I", "IE303", evaluations));
        
        evaluations =
        [
            CreateEvaluation("Midterm I", 30, 96),
            CreateEvaluation("Midterm II", 30, 100),
            CreateEvaluation("Final", 40, 100),
        ];

        AddSubject(CreateSubject("Introduction to Management", "MAN102", evaluations));
        
        evaluations =
        [
            CreateEvaluation("Quiz 1", 10, 99),
            CreateEvaluation("Midterm", 30, 96),
            CreateEvaluation("Quiz 2", 10, 100),
            CreateEvaluation("In-Lab Assignment", 15, 100),
            CreateEvaluation("Final", 35, 96),
        ];

        AddSubject(CreateSubject("Discrete Mathematics II", "MATH209", evaluations));
    }

    public IEnumerable<Subject> GetSubjects()
    {
        return _subjects;
    }

    public Subject? GetSubject(string name)
    {
        return _subjects.FirstOrDefault(s => s.Name == name);
    }

    public async Task<RepositoryActions> AddSubject(Subject subject)
    {
        if (IsSubjectInDatabase(subject.Name))
            return RepositoryActions.Conflict;
        
        _subjects.Add(subject);
        return RepositoryActions.Success;
    }

    public async Task<RepositoryActions> UpdateSubject(string name, Subject updatedSubject)
    {
        var subject = GetSubject(name);
        if (subject == null)
            return RepositoryActions.NotFound;

        subject.Name = updatedSubject.Name;
        subject.Code = updatedSubject.Code;
        // Evaluations are updated through their part of the repository
        // subject.Evaluations = updatedSubject.Evaluations;
        return RepositoryActions.Success;
    }

    public async Task<RepositoryActions> DeleteSubject(string name)
    {
        var subject = GetSubject(name);
        if(subject == null)
            return RepositoryActions.NotFound;

        _subjects.Remove(subject);
        return RepositoryActions.Success;
    }

    public IEnumerable<Evaluation> GetEvaluations(string name)
    {
        return !IsSubjectInDatabase(name) ? [] : _subjects.SelectMany(s => s.Evaluations);
    }

    public async Task<RepositoryActions> AddEvaluation(string name, Evaluation evaluation)
    {
        var subject = GetSubject(name);
        if(subject == null)
            return RepositoryActions.NotFound;
        
        subject.Evaluations.Add(evaluation);
        return RepositoryActions.Success;
    }

    public async Task<RepositoryActions> UpdateEvaluationAt(string name, int evaluationIndex, Evaluation updatedEvaluation)
    {
        var subject = GetSubject(name);
        if (subject == null || evaluationIndex >= subject.Evaluations.Count)
            return RepositoryActions.NotFound;
        
        var evaluation = subject.Evaluations[evaluationIndex];
        evaluation.Name = updatedEvaluation.Name;
        evaluation.Weight = updatedEvaluation.Weight;
        evaluation.Grade = updatedEvaluation.Grade;
        return RepositoryActions.Success;
    }

    public async Task<RepositoryActions> DeleteEvaluationAt(string name, int evaluationIndex)
    {
        var subject = GetSubject(name);
        if (subject == null || evaluationIndex >= subject.Evaluations.Count)
            return RepositoryActions.NotFound;
        
        subject.Evaluations.RemoveAt(evaluationIndex);
        return RepositoryActions.Success;
    }
    
    private bool IsSubjectInDatabase(string name)
    {
        return _subjects.Any(s => s.Name == name);
    }

    private Evaluation CreateEvaluation(string name, decimal weight, int grade)
    {
        return new Evaluation
        {
            Name = name,
            Weight = weight,
            Grade = grade,
        };
    }

    private Subject CreateSubject(string name, string code = "", IEnumerable<Evaluation>? evaluations = null)
    {
        return new Subject
        {
            Name = name,
            Code = code,
            Evaluations = evaluations == null ? [] : evaluations.ToList(),
        };
    }
}