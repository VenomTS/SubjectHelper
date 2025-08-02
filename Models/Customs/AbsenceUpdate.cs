using System;
using SubjectHelper.Helper;

namespace SubjectHelper.Models.Customs;

public class AbsenceUpdate
{
    public AbsenceTypes Type { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public int Week { get; set; }
    public int HoursMissed { get; set; }
    
    public DateOnly Date { get; set; }
}