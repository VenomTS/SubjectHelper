using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Repositories.ScheduleMaker;

public class SectionSMRepository : ISectionSMRepository
{
    private readonly DatabaseContext _dbContext;
    
    public SectionSMRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<SectionSM>> GetSectionsAsync()
    {
        return await _dbContext.SectionSM.ToListAsync();
    }

    public async Task<List<SectionSM>> GetSectionsBySubjectIdAsync(int subjectId)
    {
        return await _dbContext.SectionSM.Where(s => s.SubjectId == subjectId).ToListAsync();
    }

    public async Task<SectionSM?> GetSectionByIdAsync(int id)
    {
        return await _dbContext.SectionSM.FindAsync(id);
    }

    public async Task<SectionSM?> AddSectionAsync(SectionSM section)
    {
        if (await IsInDatabase(section.Title)) return null;
        
        await _dbContext.SectionSM.AddAsync(section);
        await _dbContext.SaveChangesAsync();
        return section;
    }

    public async Task<SectionSM?> UpdateSectionAsync(int id, SectionSM updatedSection)
    {
        var section = await _dbContext.SectionSM.FindAsync(id);
        if(section == null || await IsInDatabase(section.Title)) return null;
        
        section.Title = updatedSection.Title;
        section.Instructor = updatedSection.Instructor;
        await _dbContext.SaveChangesAsync();
        return section;
    }

    public async Task<SectionSM?> DeleteSectionAsync(int id)
    {
        var section = await _dbContext.SectionSM.FindAsync(id);
        if (section == null) return null;
        _dbContext.SectionSM.Remove(section);
        await _dbContext.SaveChangesAsync();
        return section;
    }

    private async Task<bool> IsInDatabase(string title)
    {
        return await _dbContext.SectionSM.AnyAsync(s => s.Title == title);
    }
}