using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Interfaces.ScheduleMaker;

public interface ITimeSMRepository
{
    Task<List<TimeSM>> GetTimesAsync();
    Task<List<TimeSM>> GetTimesBySectionIdAsync(int sectionId);
    Task<TimeSM?> GetTimeByIdAsync(int id);

    Task<TimeSM?> AddTimeAsync(TimeSM time);
    Task<TimeSM?> UpdateTimeAsync(int id, TimeSM updatedTime);
    Task<TimeSM?> DeleteTimeAsync(int id);
}