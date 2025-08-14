using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models;
using SubjectHelper.Models.Updates;

namespace SubjectHelper.Interfaces.Repositories;

public interface IEvaluationRepository
{
    Task<List<Evaluation>> GetEvaluationsAsync();
    Task<List<Evaluation>> GetEvaluationsBySubjectIdAsync(int subjectId);
    Task<Evaluation?> GetEvaluationAsync(int id);
    
    Task<Evaluation?> AddEvaluationAsync(Evaluation evaluation);
    Task<Evaluation?> UpdateEvaluationAsync(int id, int subjectId, EvaluationUpdate updatedEvaluation);
    Task<Evaluation?> DeleteEvaluationAsync(int id);
    Task DeleteEvaluationsBySubjectIdAsync(int subjectId);
}