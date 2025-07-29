namespace SubjectHelper.Models;

public class Evaluation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public int Grade { get; set; }

    public int SubjectId { get; set; }
}

public class EvaluationUpdate
{
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public int Grade { get; set; }
}