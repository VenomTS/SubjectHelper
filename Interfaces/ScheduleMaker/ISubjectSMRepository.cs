using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Interfaces.ScheduleMaker;

public interface ISubjectSMRepository
{
    Task<List<SubjectSM>> GetSubjectsAsync();
    Task<SubjectSM?> GetSubjectByIdAsync(int id);

    Task<SubjectSM?> AddSubjectAsync(SubjectSM subject);
    Task<SubjectSM?> UpdateSubjectAsync(int id, SubjectSM updatedSubject);
    Task<SubjectSM?> DeleteSubjectAsync(int id);
}