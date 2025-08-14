using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models;
using SubjectHelper.Models.Updates;

namespace SubjectHelper.Interfaces.Repositories;

public interface IAbsenceRepository
{
    Task<List<Absence>> GetAbsencesAsync();
    Task<List<Absence>> GetAbsencesBySubjectIdAsync(int subjectId);
    Task<Absence?> GetAbsenceAsync(int id);

    Task<Absence?> AddAbsenceAsync(Absence absence);
    Task<Absence?> UpdateAbsenceAsync(int id, int subjectId, AbsenceUpdate absenceUpdate);
    Task<Absence?> DeleteAbsenceAsync(int id);
}