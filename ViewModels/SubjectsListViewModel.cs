using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.SubjectAdd;
using SubjectHelper.Components.SubjectEdit;
using SubjectHelper.Factories;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.Models;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.ViewModels;

public partial class SubjectsListViewModel : PageViewModel
{
    private readonly ISubjectRepository _subjectRepo;
    private readonly INavigationService _navigationService;
    private readonly PageFactory _factory;
    
    public ObservableCollection<SubjectViewModel> Subjects { get; } = [];

    private readonly DialogOptions _customDialogOptions;

    public SubjectsListViewModel(INavigationService navigationService, ISubjectRepository subjectRepo, PageFactory factory)
    {
        Page = ApplicationPages.Subjects;

        _customDialogOptions = new DialogOptions
        {
            StartupLocation = WindowStartupLocation.CenterOwner,
            Mode = DialogMode.None,
            IsCloseButtonVisible = false,
            CanResize = false,
        };
        
        _subjectRepo = subjectRepo;
        _navigationService = navigationService;
        _factory = factory;

        PopulateSubjects();
    }

    private void PopulateSubjects()
    {
        foreach (var subject in _subjectRepo.GetSubjects())
        {
            var subjectViewModel = CreateSubjectViewModel(subject);
            Subjects.Add(subjectViewModel);
        }
    }

    private SubjectViewModel CreateSubjectViewModel(Subject subject)
    {
        return (SubjectViewModel) _factory.GetPageViewModel(ApplicationPages.Subject, subject.Name);
    }

    [RelayCommand]
    private async Task OpenSubjectAddFormDialog()
    {
        var vm = new SubjectAddViewModel();

        var result = await Dialog.ShowCustomModal<SubjectAddView, SubjectAddViewModel, DialogResult>(vm, options: _customDialogOptions);

        if (result != DialogResult.OK) return;

        var subject = new Subject
        {
            Name = vm.SubjectName.Trim(),
            Code = vm.SubjectCode.Trim(),
            Evaluations = [],
        };
        
        await _subjectRepo.AddSubject(subject);
        
        var subjectViewModel = CreateSubjectViewModel(subject);
        
        Subjects.Add(subjectViewModel);
    }

    [RelayCommand]
    private async Task OpenEditSubjectFormDialog(SubjectViewModel subjectViewModel)
    {
        var vm = new SubjectEditViewModel
        {
            SubjectName = subjectViewModel.Name,
            SubjectCode = subjectViewModel.Code,
        };
        
        var result = await Dialog.ShowCustomModal<SubjectEditView, SubjectEditViewModel, DialogResult>(vm, options: _customDialogOptions);
        
        if(result != DialogResult.OK) return;

        var newSubject = new Subject
        {
            Name = vm.SubjectName.Trim(),
            Code = vm.SubjectCode.Trim(),
        };
        
        await _subjectRepo.UpdateSubject(subjectViewModel.Name, newSubject);
        
        var newSubjectViewModel = CreateSubjectViewModel(newSubject);
        
        Subjects[Subjects.IndexOf(subjectViewModel)] = newSubjectViewModel;
    }

    [RelayCommand]
    private void GoToSubject(SubjectViewModel subject)
    {
        _navigationService.NavigateToPage(ApplicationPages.Subject, subject.Name);
    }

    [RelayCommand]
    private async Task DeleteSubject(SubjectViewModel subjectViewModel)
    {
        await _subjectRepo.DeleteSubject(subjectViewModel.Name);
        
        Subjects.Remove(subjectViewModel);
    }
}