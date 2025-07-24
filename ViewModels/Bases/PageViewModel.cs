using SubjectHelper.Helper;

namespace SubjectHelper.ViewModels.Bases;

public class PageViewModel : ViewModelBase
{
    public ApplicationPages Page { get; set; } = ApplicationPages.Unknown;
}