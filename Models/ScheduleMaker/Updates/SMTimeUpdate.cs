using System;

namespace SubjectHelper.Models.ScheduleMaker.Updates;

public class SMTimeUpdate
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek Day { get; set; }
}