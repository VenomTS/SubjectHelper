using System.Collections.Generic;
using SubjectHelper.ViewModels.ScheduleMaker;

namespace SubjectHelper.Interfaces.Services;

public interface IScheduleMakingService
{
    (SMSubjectViewModel?, List<List<SMSectionViewModel>>?) GenerateAllSchedules(List<SMSubjectViewModel> subjects);
}