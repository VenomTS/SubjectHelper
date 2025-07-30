using System.Collections.Generic;
using Avalonia.Media;

namespace SubjectHelper.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public List<Evaluation> Evaluations { get; set; } = [];
}

public class SubjectUpdate
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Color Color { get; set; }
}