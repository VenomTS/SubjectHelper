using System;

namespace SubjectHelper.Models.ScheduleMaker;

public class SMTime
{
    public int Id { get; set; }
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }

    public int SectionId { get; set; }
    public SMSection SMSection { get; set; }
}