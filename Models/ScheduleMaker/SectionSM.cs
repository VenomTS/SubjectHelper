using System.Collections.Generic;

namespace SubjectHelper.Models.ScheduleMaker;

public class SectionSM
{
    public int Id { get; set; }

    public int SectionId { get; set; }
    public List<TimeSM> Times { get; set; } = [];

    public int SubjectId { get; set; }
    public SubjectSM Subject { get; set; }
}