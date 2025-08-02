using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models;
using SubjectHelper.Models.Customs;

namespace SubjectHelper.Interfaces.Repositories;

public interface ISubjectRepository
{
    Task<List<Subject>> GetSubjectsAsync();
    Task<Subject?> GetSubjectByIdAsync(int id);

    Task<Subject?> AddSubjectAsync(Subject subject);
    Task<Subject?> UpdateSubjectAsync(int id, SubjectUpdate updatedSubject);
    Task<Subject?> DeleteSubjectAsync(int id);
}