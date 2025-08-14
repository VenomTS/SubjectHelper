using System;

namespace SubjectHelper.Models.ScheduleMaker;

public class TimeSM
{
    public int Id { get; set; }
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }

    public int SectionId { get; set; }
    public SectionSM Section { get; set; }
}