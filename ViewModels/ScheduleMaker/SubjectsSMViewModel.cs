using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.ScheduleMakerComponents.SectionComponent;
using SubjectHelper.Components.ScheduleMakerComponents.SubjectComponent;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels.ScheduleMaker;

public partial class SubjectsSMViewModel : PageViewModel
{

    private readonly ISubjectSMRepository _subjectRepo;
    private readonly ISectionSMRepository _sectionRepo;
    private readonly ITimeSMRepository _timeRepo;
    
    public ObservableCollection<SubjectComponentViewModel> Subjects { get; set; } = [];
    
    public SubjectsSMViewModel(ISubjectSMRepository subjectRepo, ISectionSMRepository sectionRepo, ITimeSMRepository timeRepo)
    {
        Page = ApplicationPages.ScheduleMakerSubjects;
        _subjectRepo = subjectRepo;
        _sectionRepo = sectionRepo;
        _timeRepo = timeRepo;

        var allSubjects = _subjectRepo.GetSubjectsAsync().Result;

        foreach (var subject in allSubjects)
        {
            var subjectVM = new SubjectComponentViewModel(subject, _subjectRepo, _sectionRepo, _timeRepo);
            subjectVM.OnDeletePressed += DeleteSubject;
            Subjects.Add(subjectVM);
        }
    }

    [RelayCommand]
    private async Task Add()
    {
        var newSubject = new SubjectSM
        {
            Title = "Test Title",
        };

        newSubject = await _subjectRepo.AddSubjectAsync(newSubject);
        if (newSubject is null)
            throw new NotImplementedException("Subject already exists");
        
        var subjectVM = new SubjectComponentViewModel(newSubject, _subjectRepo, _sectionRepo, _timeRepo);
        
        subjectVM.OnDeletePressed += DeleteSubject;
        
        Subjects.Add(subjectVM);
    }

    private void DeleteSubject(object? sender, EventArgs e)
    {
        if (sender is not SubjectComponentViewModel subjectVM) return;
        Subjects.Remove(subjectVM);
    }
}