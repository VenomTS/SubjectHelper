using System.Collections.Generic;

namespace SubjectHelper.Models.ScheduleMaker;

public class SectionSM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public List<TimeSM> Times { get; set; } = [];

    public int SubjectSMId { get; set; }
}