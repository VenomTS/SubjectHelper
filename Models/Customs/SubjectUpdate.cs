using Avalonia.Media;

namespace SubjectHelper.Models.Customs;

public class SubjectUpdate
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Color Color { get; set; }
}