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
    
    public async Task<List<SubjectSM>> GetSubjects()
    {
        return await _dbContext.SMSubjects.ToListAsync();
    }

    public async Task<SubjectSM?> GetSubject(int subjectId)
    {
        return await _dbContext.SMSubjects.FindAsync(subjectId);
    }

    public async Task<SubjectSM> AddSubject(string name)
    {
        var subject = new SubjectSM
        {
            Title = name,
        };
        await _dbContext.SMSubjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();

        return subject;
    }

    public async Task RemoveSubject(int subjectId)
    {
        var subject = await _dbContext.SMSubjects.FindAsync(subjectId);
        if (subject == null)
            return;
        _dbContext.Remove(subject);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<SectionSM>> GetSections(int subjectId)
    {
        return await _dbContext.SMSections.Where(x => x.SubjectId == subjectId).ToListAsync();
    }

    public async Task<SectionSM?> GetSection(int sectionId)
    {
        return await _dbContext.SMSections.FindAsync(sectionId);
    }

    public async Task<SectionSM> AddSection(int subjectId)
    {
        var subject = await GetSubject(subjectId);
        if (subject == null)
            throw new Exception("Trying to add section to a non-existent subject");
        var section = new SectionSM
        {
            SubjectId = subjectId,
            Subject = subject,
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

        _dbContext.SMSections.Remove(section);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<TimeSM>> GetTimes(int sectionId)
    {
        return await _dbContext.SMTimes.Where(x => x.SectionId == sectionId).ToListAsync();
    }

    public async Task<TimeSM?> GetTime(int timeId)
    {
        return await _dbContext.SMTimes.FindAsync(timeId);
    }

    public async Task<TimeSM> AddTime(int sectionId)
    {
        var section = await GetSection(sectionId);
        if (section == null)
            throw new Exception("Trying to add time to a non-existent section");
        var time = new TimeSM
        {
            SectionId = sectionId,
            Section = section,
        };

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