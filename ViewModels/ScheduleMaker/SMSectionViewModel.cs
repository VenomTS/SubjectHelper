using System.Collections.ObjectModel;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public partial class SMSectionViewModel : ViewModelBase
{
    public int Id { get; set; }

    public ObservableCollection<SMTimeViewModel> Times { get; set; } = [];

    public int SMSectionId { get; set; }
    public int SMSubjectId { get; set; }

    public SMSectionViewModel()
    {
        
    }

    public SMSectionViewModel(SMSection section)
    {
        Id = section.Id;
        SMSectionId = section.SectionId;
        SMSubjectId = section.SMSubjectId;
        foreach (var time in section.Times)
        {
            Times.Add(new SMTimeViewModel(time));
        }
    }
}