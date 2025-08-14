using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Interfaces.ScheduleMaker;

public interface IScheduleMakerRepository
{
    // Subject related
    Task<List<SubjectSM>> GetSubjects();
    Task<SubjectSM?> GetSubject(int subjectId);
    Task<SubjectSM> AddSubject(string name);
    Task RemoveSubject(int subjectId);
    // Task UpdateSubject()
    
    // Section related
    Task<List<SectionSM>> GetSections(int subjectId);
    Task<SectionSM?> GetSection(int sectionId);
    Task<SectionSM> AddSection(int subjectId);
    Task RemoveSection(int sectionId);
    // Task UpdateSection()
    
    // Time related
    Task<List<TimeSM>> GetTimes(int sectionId);
    Task<TimeSM?> GetTime(int timeId);
    Task<TimeSM> AddTime(int sectionId);
    Task RemoveTime(int timeId);
    
    // Task UpdateTime();
}