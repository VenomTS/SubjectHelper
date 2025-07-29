using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubjectHelper.Components.ScheduleMakerComponents.SectionComponent;
using SubjectHelper.Components.ScheduleMakerComponents.SectionForm;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Models.ScheduleMaker;
using SubjectHelper.ViewModels.Bases;
using Ursa.Controls;

namespace SubjectHelper.Components.ScheduleMakerComponents.SubjectComponent;

public partial class SubjectComponentViewModel : ViewModelBase
{
    private SubjectSM _subject;
    
    private readonly ISubjectSMRepository _subjectRepo;
    private readonly ISectionSMRepository _sectionRepo;
    private readonly ITimeSMRepository _timeRepo;
    
    [ObservableProperty] private string _title;

    public ObservableCollection<SectionComponentViewModel> Sections { get; set; } = [];

    public event EventHandler? OnDeletePressed;
    
    private static readonly DialogOptions DefaultDialogOptions = new()
    {
        CanResize = false,
        IsCloseButtonVisible = false,
        Mode = DialogMode.None,
        StartupLocation = WindowStartupLocation.CenterOwner,
    };
    
    public SubjectComponentViewModel(SubjectSM subject, ISubjectSMRepository subjectRepo, ISectionSMRepository sectionRepo, ITimeSMRepository timeRepo)
    {
        _subject = subject;
        _subjectRepo = subjectRepo;
        _sectionRepo = sectionRepo;
        _timeRepo = timeRepo;

        _title = _subject.Title;

        foreach (var section in _subject.Sections)
        {
            var sectionVM = new SectionComponentViewModel(_sectionRepo, _timeRepo, section);
            sectionVM.OnDeletePressed += DeleteSection;
            
            Sections.Add(sectionVM);
        }
    }

    [RelayCommand]
    private async Task AddSection()
    {
        var sectionVM = new SectionFormViewModel("Add Section");
        
        var result = await Dialog.ShowCustomModal<SectionFormView, SectionFormViewModel, DialogResult>(sectionVM, null, DefaultDialogOptions);

        if (result != DialogResult.OK) return;

        var section = new SectionSM
        {
            Title = sectionVM.Title,
            Instructor = sectionVM.Instructor,
        };

        var newSection = await _sectionRepo.AddSectionAsync(section);
        if (newSection == null)
            throw new NotImplementedException("Section already exists");

        _subject.Sections.Add(newSection);

        var newSectionVM = new SectionComponentViewModel(_sectionRepo, _timeRepo, newSection);
        newSectionVM.OnDeletePressed += DeleteSection;
        
        Sections.Add(newSectionVM);
    }

    [RelayCommand]
    private async Task Delete()
    {
        var allSections = await _sectionRepo.GetSectionsBySubjectIdAsync(_subject.Id);
        foreach (var section in allSections)
        {
            var allTimes = await _timeRepo.GetTimesBySectionIdAsync(section.Id);
            foreach (var time in allTimes)
                await _timeRepo.DeleteTimeAsync(time.Id);
            await _sectionRepo.DeleteSectionAsync(section.Id);
        }
        await _subjectRepo.DeleteSubjectAsync(_subject.Id);
        OnDeletePressed?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private async Task Edit()
    {
        Console.WriteLine("Editing :)");
    }

    private void DeleteSection(object? sender, EventArgs e)
    {
        if (sender is not SectionComponentViewModel vm) return;
        Sections.Remove(vm);
    }
}