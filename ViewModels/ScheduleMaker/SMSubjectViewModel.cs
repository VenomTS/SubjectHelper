using System.Collections.ObjectModel;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public class SMSubjectViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public ObservableCollection<SMSectionViewModel> Sections { get; set; } = [];
}