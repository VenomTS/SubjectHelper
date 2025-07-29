using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Repositories.ScheduleMaker;

public class TimeSMRepository : ITimeSMRepository
{
    private readonly DatabaseContext _dbContext;
    
    public TimeSMRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<TimeSM>> GetTimesAsync()
    {
        return await _dbContext.TimeSM.ToListAsync();
    }

    public async Task<List<TimeSM>> GetTimesBySectionIdAsync(int sectionId)
    {
        return await _dbContext.TimeSM.Where(s => s.SectionSMId == sectionId).ToListAsync();
    }

    public async Task<TimeSM?> GetTimeByIdAsync(int id)
    {
        return await _dbContext.TimeSM.FindAsync(id);
    }

    public async Task<TimeSM?> AddTimeAsync(TimeSM time)
    {
        await _dbContext.TimeSM.AddAsync(time);
        await _dbContext.SaveChangesAsync();
        return time;
    }

    public async Task<TimeSM?> UpdateTimeAsync(int id, TimeSM updatedTime)
    {
        var time = await _dbContext.TimeSM.FindAsync(id);
        if(time == null) return null;
        
        time.Day = updatedTime.Day;
        time.StartTime = updatedTime.StartTime;
        time.EndTime = updatedTime.EndTime;
        await _dbContext.SaveChangesAsync();
        return time;
    }

    public async Task<TimeSM?> DeleteTimeAsync(int id)
    {
        var time = await _dbContext.TimeSM.FindAsync(id);
        if (time == null) return null;
        _dbContext.TimeSM.Remove(time);
        await _dbContext.SaveChangesAsync();
        return time;
    }
}