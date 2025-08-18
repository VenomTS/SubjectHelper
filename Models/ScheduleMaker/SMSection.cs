using System.Collections.Generic;

namespace SubjectHelper.Models.ScheduleMaker;

public class SMSection
{
    public int Id { get; set; }

    public int SectionId { get; set; }
    public List<SMTime> Times { get; set; } = [];

    public int SMSubjectId { get; set; }
    public SMSubject? SMSubject { get; set; }
}