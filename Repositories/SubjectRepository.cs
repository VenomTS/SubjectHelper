using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces;
using SubjectHelper.Interfaces.Repositories;
using SubjectHelper.Models;
using SubjectHelper.Models.Customs;

namespace SubjectHelper.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly DatabaseContext _dbContext;

    public SubjectRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public async Task<List<Subject>> GetSubjectsAsync()
    {
        return await _dbContext.Subjects.ToListAsync();
    }

    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        return await _dbContext.Subjects.FindAsync(id);
    }

    public async Task<Subject?> AddSubjectAsync(Subject subject)
    {
        if (await _dbContext.Subjects.AnyAsync(s => s.Name == subject.Name))
            return null;
        
        await _dbContext.Subjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();
        return subject;
    }

    public async Task<Subject?> UpdateSubjectAsync(int id, SubjectUpdate updatedSubject)
    {
        var subject = await _dbContext.Subjects.FindAsync(id);
        var subjectExists = await _dbContext.Subjects.AnyAsync(s => s.Id != id && s.Name == updatedSubject.Name);
        if (subject == null || subjectExists) return null;
        
        subject.Name = updatedSubject.Name;
        subject.Code = updatedSubject.Code;
        subject.Color = ColorToHexConverter.ToHexString(updatedSubject.Color, AlphaComponentPosition.Trailing, includeAlpha: false);

        await _dbContext.SaveChangesAsync();
        return subject;
    }

    public async Task<Subject?> DeleteSubjectAsync(int id)
    {
        var subject = await _dbContext.Subjects.FindAsync(id);
        if (subject == null) return null;

        var affectedEvaluations = await _dbContext.Evaluations.Where(eval => eval.SubjectId == id).ToListAsync();
        foreach (var eval in affectedEvaluations)
            _dbContext.Evaluations.Remove(eval);
        
        
        _dbContext.Subjects.Remove(subject);
        await _dbContext.SaveChangesAsync();
        return subject;
    }
}