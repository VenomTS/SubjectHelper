using System;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public class SMDisplayViewModel
{
    public DayOfWeek Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string SectionFullName { get; set; } = string.Empty;
}