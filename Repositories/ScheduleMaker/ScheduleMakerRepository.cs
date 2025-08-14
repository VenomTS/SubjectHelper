using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Repositories.ScheduleMaker;

public class ScheduleMakerRepository : IScheduleMakerRepository
{
    private readonly DatabaseContext _dbContext;

    public ScheduleMakerRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<SMSubject>> GetSubjects()
    {
        return await _dbContext.SMSubjects.Include(x => x.Sections).ThenInclude(x => x.Times).ToListAsync();
    }

    public async Task<SMSubject?> GetSubject(int subjectId)
    {
        return await _dbContext.SMSubjects.Where(x => x.Id == subjectId).Include(x => x.Sections)
            .ThenInclude(x => x.Times).FirstOrDefaultAsync();
    }

    public async Task<SMSubject> AddSubject(string name)
    {
        var subject = new SMSubject
        {
            Title = name,
        };
        await _dbContext.SMSubjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();

        return subject;
    }

    public async Task RemoveSubject(int subjectId)
    {
        var subject = await GetSubject(subjectId);
        if (subject == null)
            return;

        var affectedSections = await _dbContext.SMSections.Where(x => x.SubjectId == subjectId).ToListAsync();

        foreach (var section in affectedSections)
            await RemoveSection(section.Id);
        
        _dbContext.Remove(subject);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<SMSection>> GetSections(int subjectId)
    {
        return await _dbContext.SMSections.Where(x => x.SubjectId == subjectId).Include(x => x.Times).ToListAsync();
    }

    public async Task<SMSection?> GetSection(int sectionId)
    {
        return await _dbContext.SMSections.Where(x => x.Id == sectionId).Include(x => x.Times).FirstOrDefaultAsync();
    }

    public async Task<SMSection> AddSection(int subjectId)
    {
        var subject = await GetSubject(subjectId);
        if (subject == null)
            throw new Exception("Trying to add section to a non-existent subject");
        var section = new SMSection
        {
            SubjectId = subjectId,
            SMSubject = subject,
            SectionId = subject.GetAvailableSectionId(),
        };

        await _dbContext.SMSections.AddAsync(section);
        await _dbContext.SaveChangesAsync();

        return section;
    }

    public async Task RemoveSection(int sectionId)
    {
        var section = await GetSection(sectionId);
        if (section == null)
            return;

        var affectedTimes = await _dbContext.SMTimes.Where(x => x.SectionId == sectionId).ToListAsync();

        foreach (var time in affectedTimes)
            await RemoveTime(time.Id);
        
        _dbContext.SMSections.Remove(section);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<SMTime>> GetTimes(int sectionId)
    {
        return await _dbContext.SMTimes.Where(x => x.SectionId == sectionId).ToListAsync();
    }

    public async Task<SMTime?> GetTime(int timeId)
    {
        return await _dbContext.SMTimes.FindAsync(timeId);
    }

    public async Task<SMTime> AddTime(int sectionId, SMTime time)
    {
        var section = await GetSection(sectionId);
        if (section == null)
            throw new Exception("Trying to add time to a non-existent section");

        time.SMSection = section;

        await _dbContext.SMTimes.AddAsync(time);
        await _dbContext.SaveChangesAsync();
        return time;
    }

    public async Task RemoveTime(int timeId)
    {
        var time = await GetTime(timeId);
        if (time == null)
            return;

        _dbContext.Remove(time);
        await _dbContext.SaveChangesAsync();
    }
}