using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces.Repositories;
using SubjectHelper.Models;
using SubjectHelper.Models.Customs;

namespace SubjectHelper.Repositories;

public class EvaluationRepository : IEvaluationRepository
{
    private readonly DatabaseContext _dbContext;

    public EvaluationRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Evaluation>> GetEvaluationsAsync()
    {
        return await _dbContext.Evaluations.ToListAsync();
    }

    public async Task<List<Evaluation>> GetEvaluationsBySubjectIdAsync(int subjectId)
    {
        var evaluations = await _dbContext.Evaluations.Where(e => e.SubjectId == subjectId).ToListAsync();
        return evaluations;
    }

    public async Task<Evaluation?> GetEvaluationAsync(int id)
    {
        return await _dbContext.Evaluations.FindAsync(id);
    }

    public async Task<Evaluation?> AddEvaluationAsync(Evaluation evaluation)
    {
        var isInDatabase = await _dbContext.Evaluations.AnyAsync(e => e.SubjectId == evaluation.SubjectId && e.Name == evaluation.Name);
        if(isInDatabase) return null;
        
        await _dbContext.Evaluations.AddAsync(evaluation);
        await _dbContext.SaveChangesAsync();
        return evaluation;
    }

    public async Task<Evaluation?> UpdateEvaluationAsync(int id, int subjectId, EvaluationUpdate updatedEvaluation)
    {
        var evaluation = await _dbContext.Evaluations.FindAsync(id);
        var isInDatabase = await _dbContext.Evaluations.AnyAsync(e => e.Id != id && e.SubjectId == subjectId && e.Name == updatedEvaluation.Name);
        if (evaluation == null || isInDatabase) return null;
        
        evaluation.Name = updatedEvaluation.Name;
        evaluation.Weight = updatedEvaluation.Weight;
        evaluation.Grade = updatedEvaluation.Grade;
        await _dbContext.SaveChangesAsync();
        
        return evaluation;
    }

    public async Task<Evaluation?> DeleteEvaluationAsync(int id)
    {
        var evaluation = await _dbContext.Evaluations.FindAsync(id);
        if (evaluation == null) return null;
        
        _dbContext.Evaluations.Remove(evaluation);
        await _dbContext.SaveChangesAsync();
        return evaluation;
    }

    public async Task DeleteEvaluationsBySubjectIdAsync(int subjectId)
    {
        var evaluations = await GetEvaluationsBySubjectIdAsync(subjectId);
        foreach(var evaluation in evaluations)
            _dbContext.Evaluations.Remove(evaluation);
        await _dbContext.SaveChangesAsync();
    }
    
}