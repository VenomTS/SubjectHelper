using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.SubjectAdd;
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
        var dialogOptions = new DialogOptions
        {
            // Title = "Add Subject",
            StartupLocation = WindowStartupLocation.CenterOwner,
            Mode = DialogMode.None,
            // Button = DialogButton.OKCancel,
            CanResize = false,
        };

        var vm = new SubjectAddViewModel();

        var result = await Dialog.ShowCustomModal<SubjectAddView, SubjectAddViewModel, DialogResult>(vm, options: dialogOptions);

        if (result != DialogResult.OK) return;

        var subject = new Subject
        {
            Name = vm.SubjectName,
            Code = vm.SubjectCode,
            Evaluations = [],
        };
        
        _subjectRepo.AddSubject(subject);
        
        var subjectViewModel = CreateSubjectViewModel(subject);
        
        Subjects.Add(subjectViewModel);
    }
    
    // [RelayCommand]
    // private async Task OpenEditEvaluationFormDialog(EvaluationViewModel evaluationViewModel)
    // {
    //     var dialogOptions = new DialogOptions
    //     {
    //         Title = "Edit Evaluation",
    //         StartupLocation = WindowStartupLocation.CenterOwner,
    //         Mode = DialogMode.None,
    //         Button = DialogButton.OKCancel,
    //         CanResize = false,
    //     };
    //     
    //     var vm = new EvaluationFormViewModel(evaluationViewModel.Name, evaluationViewModel.Weight, evaluationViewModel.Grade);
    //     
    //     var result = await Dialog.ShowModal<EvaluationFormView, EvaluationFormViewModel>(vm, options: dialogOptions);
    //     
    //     if(result != DialogResult.OK) return;
    //     
    //     var evaluationIndex = Evaluations.IndexOf(evaluationViewModel);
    //
    //     if (evaluationIndex == -1)
    //         throw new Exception("Evaluation not found although it should be present");
    //     
    //     var newEvaluation = CreateEvaluation(vm.Title, vm.Weight, vm.Grade);
    //     
    //     if(_subject == null)
    //         throw new Exception("Subject is undefined");
    //     
    //     _subject.Evaluations[evaluationIndex] = newEvaluation;
    //     
    //     Evaluations[evaluationIndex] = new EvaluationViewModel(newEvaluation);
    //     
    //     CalculateWeightedGrade();
    // }

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