using System;

namespace SubjectHelper.Models.ScheduleMaker;

public class TimeSM
{
    public int Id { get; set; }
    public int Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int SectionSMId { get; set; }
}