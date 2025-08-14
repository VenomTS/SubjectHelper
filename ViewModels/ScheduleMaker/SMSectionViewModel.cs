using System.Collections.ObjectModel;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public partial class SMSectionViewModel : ViewModelBase
{
    public int Id { get; set; }

    public ObservableCollection<SMTimeViewModel> Times { get; set; } = [];
}