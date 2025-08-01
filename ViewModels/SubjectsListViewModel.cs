using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.SubjectForm;
using SubjectHelper.Factories;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces.Repositories;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.Models;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.ViewModels;

public partial class SubjectsListViewModel : PageViewModel
{
    private readonly ISubjectRepository _subjectRepo;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly IToastService _toastService;
    private readonly PageFactory _factory;
    
    public ObservableCollection<SubjectViewModel> Subjects { get; } = [];

    public SubjectsListViewModel(INavigationService navigationService, IDialogService dialogService, IToastService toastService, ISubjectRepository subjectRepo, PageFactory factory)
    {
        Page = ApplicationPages.Subjects;
        
        _subjectRepo = subjectRepo;
        _navigationService = navigationService;
        _dialogService = dialogService;
        _toastService = toastService;
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
    private async Task AddSubject()
    {
        var vm = new SubjectFormViewModel();

        var result = await _dialogService.ShowSubjectForm("Add Subject", vm);

        if (result != DialogResult.OK) return;

        var subject = await _subjectRepo.AddSubjectAsync(new Subject
        {
            Name = vm.Title,
            Code = vm.Code,
            Color = ColorToHexConverter.ToHexString(vm.Color, AlphaComponentPosition.Leading, includeAlpha: false),
        });

        if (subject == null)
        {
            _toastService.ShowToast("Subject Already Exists", NotificationType.Error);
            return;
        }

        var subjectViewModel = CreateSubjectViewModel(subject);
        Subjects.Add(subjectViewModel);
        _toastService.ShowToast("Subject Created", NotificationType.Success);
    }

    [RelayCommand]
    private async Task EditSubject(SubjectViewModel subjectVM)
    {
        
        var vm = new SubjectFormViewModel
        {
            Title = subjectVM.Name,
            Code = subjectVM.Code,
            Color = subjectVM.Color,
        };

        var result = await _dialogService.ShowSubjectForm("Edit Subject", vm);

        if (result != DialogResult.OK) return;

        var subject = await _subjectRepo.UpdateSubjectAsync(subjectVM.SubjectId, new SubjectUpdate
        {
            Name = vm.Title,
            Code = vm.Code,
            Color = vm.Color,
        });

        if (subject == null)
        {
            _toastService.ShowToast("Subject Already Exists", NotificationType.Error);
            return;
        }

        var newSubjectVM = CreateSubjectViewModel(subject);

        var subjectIndex = Subjects.IndexOf(subjectVM);
        Subjects[subjectIndex] = newSubjectVM;
        
        _toastService.ShowToast("Subject Edited", NotificationType.Success);
    }

    [RelayCommand]
    private void GoToSubject(SubjectViewModel subject) => _navigationService.NavigateToPage(ApplicationPages.Subject, subject.SubjectId);

    [RelayCommand]
    private async Task DeleteSubject(SubjectViewModel subjectViewModel)
    {
        await _subjectRepo.DeleteSubjectAsync(subjectViewModel.SubjectId);
        
        Subjects.Remove(subjectViewModel);
        
        _toastService.ShowToast("Subject Deleted", NotificationType.Success);
    }
}