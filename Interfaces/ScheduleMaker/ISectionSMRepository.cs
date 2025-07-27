using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.Interfaces.ScheduleMaker;

public interface ISectionSMRepository
{
    Task<List<SectionSM>> GetSectionsAsync();
    Task<List<SectionSM>> GetSectionsBySubjectIdAsync(int subjectId);
    Task<SectionSM?> GetSectionByIdAsync(int id);

    Task<SectionSM?> AddSectionAsync(SectionSM section);
    Task<SectionSM?> UpdateSectionAsync(int id, SectionSM updatedSection);
    Task<SectionSM?> DeleteSectionAsync(int id);
}