using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Helper;
using SubjectHelper.Models;

namespace SubjectHelper.Interfaces;

public interface ISubjectRepository
{
    Task<List<Subject>> GetSubjectsAsync();
    Task<Subject?> GetSubjectByIdAsync(int id);

    Task<Subject?> AddSubjectAsync(Subject subject);
    Task<Subject?> UpdateSubjectAsync(int id, Subject updatedSubject);
    Task<Subject?> DeleteSubjectAsync(int id);
}