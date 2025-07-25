using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
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
    private readonly IEvaluationRepository _evaluationRepo;
    private readonly INavigationService _navigationService;
    private readonly PageFactory _factory;
    
    public ObservableCollection<SubjectViewModel> Subjects { get; } = [];

    public WindowToastManager? ToastManager { get; set; }

    private static readonly DialogOptions CustomDialogOptions = new()
    {
        StartupLocation = WindowStartupLocation.CenterOwner,
        Mode = DialogMode.None,
        IsCloseButtonVisible = false,
        CanResize = false,
    };

    public SubjectsListViewModel(INavigationService navigationService, ISubjectRepository subjectRepo, PageFactory factory, IEvaluationRepository evaluationRepo)
    {
        Page = ApplicationPages.Subjects;
        
        _subjectRepo = subjectRepo;
        _evaluationRepo = evaluationRepo;
        _navigationService = navigationService;
        _factory = factory;

        _ = PopulateSubjects();
    }

    private async Task PopulateSubjects()
    {
        var allSubjects = await _subjectRepo.GetSubjectsAsync();
        foreach (var subjectVM in allSubjects.Select(CreateSubjectViewModel))
        {
            Subjects.Add(subjectVM);
        }
    }

    private SubjectViewModel CreateSubjectViewModel(Subject subject)
    {
        return (SubjectViewModel) _factory.GetPageViewModel(ApplicationPages.Subject, subject.Id);
    }

    [RelayCommand]
    private async Task OpenSubjectAddFormDialog()
    {
        var vm = new SubjectAddViewModel();

        var result = await Dialog.ShowCustomModal<SubjectAddView, SubjectAddViewModel, DialogResult>(vm, options: CustomDialogOptions);

        if (result != DialogResult.OK) return;

        var subject = new Subject
        {
            Name = vm.SubjectName.Trim(),
            Code = vm.SubjectCode.Trim(),
            Evaluations = [],
        };
        
        subject = await _subjectRepo.AddSubjectAsync(subject);

        if (subject == null)
        {
            ToastManager?.Show(ToastCreator.CreateToast("Subject already exists", NotificationType.Error));
            return;
        }
        
        var subjectViewModel = CreateSubjectViewModel(subject);
        
        Subjects.Add(subjectViewModel);
        
        ToastManager?.Show(ToastCreator.CreateToast("Subject added", NotificationType.Success));
    }

    [RelayCommand]
    private async Task OpenEditSubjectFormDialog(SubjectViewModel subjectViewModel)
    {
        var vm = new SubjectEditViewModel
        {
            SubjectName = subjectViewModel.Name,
            SubjectCode = subjectViewModel.Code,
        };
        
        var result = await Dialog.ShowCustomModal<SubjectEditView, SubjectEditViewModel, DialogResult>(vm, options: CustomDialogOptions);
        
        if(result != DialogResult.OK) return;

        var newSubject = new Subject
        {
            Name = vm.SubjectName.Trim(),
            Code = vm.SubjectCode.Trim(),
        };
        
        newSubject = await _subjectRepo.UpdateSubjectAsync(subjectViewModel.Id, newSubject);
        
        if (newSubject == null)
        {
            ToastManager?.Show(ToastCreator.CreateToast("Subject already exists", NotificationType.Error));
            return;
        }
        
        var newSubjectViewModel = CreateSubjectViewModel(newSubject);
        
        Subjects[Subjects.IndexOf(subjectViewModel)] = newSubjectViewModel;
        
        ToastManager?.Show(ToastCreator.CreateToast("Subject edited", NotificationType.Success));
    }

    [RelayCommand]
    private void GoToSubject(SubjectViewModel subject)
    {
        _navigationService.NavigateToPage(ApplicationPages.Subject, subject.Id);
    }

    [RelayCommand]
    private async Task DeleteSubject(SubjectViewModel subjectViewModel)
    {
        await _evaluationRepo.DeleteEvaluationsBySubjectIdAsync(subjectViewModel.Id);
        await _subjectRepo.DeleteSubjectAsync(subjectViewModel.Id);
        
        Subjects.Remove(subjectViewModel);
        
        ToastManager?.Show(ToastCreator.CreateToast("Subject deleted", NotificationType.Warning));
    }
}