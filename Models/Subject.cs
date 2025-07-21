using System.Collections.Generic;

namespace SubjectHelper.Models;

public class Subject
{
    public string Name { get; set; } = string.Empty;
    public List<Evaluation> Evaluations { get; set; } = [];
}