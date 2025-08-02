using System;
using SubjectHelper.Helper;

namespace SubjectHelper.Models;

public class Absence
{
    public int Id { get; set; }
    public AbsenceTypes Type { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public int Week { get; set; }
    public int HoursMissed { get; set; }
    
    public DateOnly Date { get; set; }

    public int SubjectId { get; set; }
}