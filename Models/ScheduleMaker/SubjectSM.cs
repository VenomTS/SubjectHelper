using System.Collections.Generic;

namespace SubjectHelper.Models.ScheduleMaker;

public class SubjectSM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<SectionSM> Sections { get; set; } = [];
}