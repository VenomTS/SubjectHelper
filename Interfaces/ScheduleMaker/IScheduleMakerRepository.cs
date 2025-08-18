using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker.Updates;

namespace SubjectHelper.Interfaces.ScheduleMaker;

public interface IScheduleMakerRepository
{
    // Subject related
    Task<List<SMSubject>> GetSubjects();
    Task<SMSubject?> GetSubject(int subjectId);
    Task<SMSubject> AddSubject(string name);
    Task<SMSubject?> UpdateSubject(int subjectId, SMSubjectUpdate subjectUpdate);
    Task RemoveSubject(int subjectId);
    // Task UpdateSubject()
    
    // Section related
    Task<List<SMSection>> GetSections(int subjectId);
    Task<SMSection?> GetSection(int sectionId);
    Task<SMSection> AddSection(int subjectId);
    Task RemoveSection(int sectionId);
    // Task UpdateSection()
    
    // Time related
    Task<List<SMTime>> GetTimes(int sectionId);
    Task<SMTime?> GetTime(int timeId);
    Task<SMTime> AddTime(int sectionId, SMTime time);
    Task<SMTime?> UpdateTime(int timeId, SMTimeUpdate timeUpdate);
    Task RemoveTime(int timeId);
    
    // Task UpdateTime();
}