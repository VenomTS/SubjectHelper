using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Repositories.ScheduleMaker;

public class SubjectSMRepository : ISubjectSMRepository
{
    private readonly DatabaseContext _dbContext;
    
    public SubjectSMRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<SubjectSM>> GetSubjectsAsync()
    {
        return await _dbContext.SubjectSM.ToListAsync();
    }

    public async Task<SubjectSM?> GetSubjectByIdAsync(int id)
    {
        return await _dbContext.SubjectSM.FindAsync(id);
    }

    public async Task<SubjectSM?> AddSubjectAsync(SubjectSM subject)
    {
        if (await IsInDatabase(subject.Title)) return null;
        
        await _dbContext.SubjectSM.AddAsync(subject);
        await _dbContext.SaveChangesAsync();
        return subject;
    }

    public async Task<SubjectSM?> UpdateSubjectAsync(int id, SubjectSM updatedSubject)
    {
        var subject = await _dbContext.SubjectSM.FindAsync(id);
        if(subject == null || await IsInDatabase(subject.Title)) return null;
        
        subject.Title = updatedSubject.Title;
        await _dbContext.SaveChangesAsync();
        return subject;
    }

    public async Task<SubjectSM?> DeleteSubjectAsync(int id)
    {
        var subject = await _dbContext.SubjectSM.FindAsync(id);
        if (subject == null) return null;
        _dbContext.SubjectSM.Remove(subject);
        await _dbContext.SaveChangesAsync();
        return subject;
    }

    private async Task<bool> IsInDatabase(string title)
    {
        return await _dbContext.SubjectSM.AnyAsync(s => s.Title == title);
    }
}