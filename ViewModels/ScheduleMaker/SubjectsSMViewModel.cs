using System;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Components.ScheduleMakerComponents.SectionComponent;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public partial class SubjectsSMViewModel : PageViewModel
{
    [ObservableProperty] private SectionComponentViewModel _section;
    
    public SubjectsSMViewModel(ISectionSMRepository sectionRepo, ITimeSMRepository timeRepo)
    {
        Page = ApplicationPages.ScheduleMakerSubjects;

        _section = new SectionComponentViewModel(sectionRepo, timeRepo, new SectionSM());
    }
}