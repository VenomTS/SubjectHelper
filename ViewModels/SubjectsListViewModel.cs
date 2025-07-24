using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Factories;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.ViewModels;

public partial class SubjectsListViewModel : PageViewModel
{
    private readonly ISubjectRepository _subjectRepo;
    private readonly INavigationService _navigationService;
    private readonly PageFactory _factory;
    
    public ObservableCollection<SubjectViewModel> Subjects { get; } = [];

    public SubjectsListViewModel(INavigationService navigationService, ISubjectRepository subjectRepo, PageFactory factory)
    {
        Page = ApplicationPages.Subjects;
        
        _subjectRepo = subjectRepo;
        _navigationService = navigationService;
        _factory = factory;

        PopulateSubjects();
    }

    private void PopulateSubjects()
    {
        foreach (var subject in _subjectRepo.GetSubjects())
        {
            var subjectViewModel = (SubjectViewModel) _factory.GetPageViewModel(ApplicationPages.Subject, subject.Name);
            Subjects.Add(subjectViewModel);
        }
    }

    [RelayCommand]
    private void GoToSubject(SubjectViewModel subject)
    {
        _navigationService.NavigateToPage(ApplicationPages.Subject, subject.Name);
    }

    [RelayCommand]
    private void DeleteSubject(SubjectViewModel subjectViewModel)
    {
        var subject = _subjectRepo.GetSubject(subjectViewModel.Name);
        
        if(subject == null)
            throw new Exception("Subject not found");

        _subjectRepo.DeleteSubject(subject);
        
        Subjects.Remove(subjectViewModel);
    }
}