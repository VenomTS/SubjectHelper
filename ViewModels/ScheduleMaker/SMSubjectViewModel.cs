using System.Collections.ObjectModel;
using SubjectHelper.Models.ScheduleMaker;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public class SMSubjectViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public ObservableCollection<SMSectionViewModel> Sections { get; set; } = [];

    public SMSubjectViewModel()
    {
        
    }

    public SMSubjectViewModel(SMSubject subject)
    {
        Id = subject.Id;
        Title = subject.Title;

        foreach (var section in subject.Sections)
        {
            Sections.Add(new SMSectionViewModel(section));
        }
    }
}