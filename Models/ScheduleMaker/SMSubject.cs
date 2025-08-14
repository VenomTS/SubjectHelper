using System.Collections.Generic;

namespace SubjectHelper.Models.ScheduleMaker;

public class SMSubject
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public List<SMSection> Sections { get; set; } = [];

    public int GetAvailableSectionId()
    {
        var n = Sections.Count;
        var seen = new bool[n + 1];

        foreach (var section in Sections)
        {
            if (section.SectionId >= n + 1) continue;
            seen[section.SectionId] = true;
        }
        
        for(var i = 1; i <= n; i++)
            if (!seen[i])
                return i;

        return n + 1;
    }
}