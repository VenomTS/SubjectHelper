using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Helper;
using SubjectHelper.Models;

namespace SubjectHelper.Interfaces;

public interface ISubjectRepository
{
    // Subject related stuff
    Task<List<Subject>> GetSubjectsAsync();
    Task<Subject?> GetSubjectByIdAsync(int id);
    // Task<Subject?> GetSubjectByNameAsync(string name);

    Task<Subject?> AddSubjectAsync(Subject subject);
    Task<Subject?> UpdateSubjectAsync(int id, Subject updatedSubject);
    // Task<Subject?> UpdateSubjectAsync(string name, Subject updatedSubject);
    Task<Subject?> DeleteSubjectAsync(int id);
    // Task<Subject?> DeleteSubjectAsync(string name);
}