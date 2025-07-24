using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Helper;
using SubjectHelper.Models;

namespace SubjectHelper.Interfaces;

public interface ISubjectRepository
{
    // Subject related stuff
    // Name is used as unique identified
    IEnumerable<Subject> GetSubjects();
    Subject? GetSubject(string name);

    Task<RepositoryActions> AddSubject(Subject subject);
    Task<RepositoryActions> UpdateSubject(string name, Subject updatedSubject);
    Task<RepositoryActions> DeleteSubject(string name);
    
    // Evaluation related stuff
    // Name argument refers to a subject to which the evaluation is relatred
    IEnumerable<Evaluation> GetEvaluations(string name);

    Task<RepositoryActions> AddEvaluation(string name, Evaluation evaluation);
    Task<RepositoryActions> UpdateEvaluationAt(string name, int evaluationIndex, Evaluation updatedEvaluation);
    Task<RepositoryActions> DeleteEvaluationAt(string name, int evaluationIndex);
}