using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectHelper.Data;
using SubjectHelper.Interfaces.Repositories;
using SubjectHelper.Models;
using SubjectHelper.Models.Customs;

namespace SubjectHelper.Repositories;

public class AbsenceRepository : IAbsenceRepository
{

    private readonly DatabaseContext _dbContext;

    public AbsenceRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Absence>> GetAbsencesAsync()
    {
        return await _dbContext.Absences.ToListAsync();
    }

    public async Task<List<Absence>> GetAbsencesBySubjectIdAsync(int subjectId)
    {
        return await _dbContext.Absences.Where(x => x.SubjectId == subjectId).ToListAsync();
    }

    public async Task<Absence?> GetAbsenceAsync(int id)
    {
        return await _dbContext.Absences.FindAsync(id);
    }

    public async Task<Absence?> AddAbsenceAsync(Absence absence)
    {
        var exists = await _dbContext.Absences.AnyAsync(x => x.Date == absence.Date && x.Type == absence.Type);
        if (exists) return null;

        await _dbContext.Absences.AddAsync(absence);
        await _dbContext.SaveChangesAsync();

        return absence;
    }

    public async Task<Absence?> UpdateAbsenceAsync(int id, int subjectId, AbsenceUpdate absenceUpdate)
    {
        var absence = await _dbContext.Absences.FindAsync(id);
        var exists =
            await _dbContext.Absences.AnyAsync(x => x.Date == absenceUpdate.Date && x.Type == absenceUpdate.Type && x.Week == absenceUpdate.Week && x.SubjectId == subjectId);
        if (absence == null || exists) return null;

        absence.Type = absenceUpdate.Type;
        absence.Title = absenceUpdate.Title;
        absence.Week = absenceUpdate.Week;
        absence.HoursMissed = absenceUpdate.HoursMissed;
        absence.Date = absenceUpdate.Date;

        await _dbContext.SaveChangesAsync();

        return absence;
    }

    public async Task<Absence?> DeleteAbsenceAsync(int id)
    {
        var absence = await _dbContext.Absences.FindAsync(id);
        if (absence == null) return null;
        _dbContext.Absences.Remove(absence);
        await _dbContext.SaveChangesAsync();

        return absence;
    }
}