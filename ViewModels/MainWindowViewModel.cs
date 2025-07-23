using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using SubjectHelper.Interfaces;

namespace SubjectHelper.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<TabItem> TabItems { get; } = [];

    [ObservableProperty] private TabItem _selectedTabItem;

    private readonly ISubjectRepository _subjectRepo;

    public MainWindowViewModel(ISubjectRepository subjectRepository)
    {
        _subjectRepo = subjectRepository;

        var subjects = _subjectRepo.GetSubjects().ToList();

        foreach (var subject in subjects)
        {
            var tabItem = new TabItem
            {
                Header = subject.Name,
                Content = new SubjectViewModel(subject)
            };
            TabItems.Add(tabItem);
        }
        
        _selectedTabItem = TabItems.First();
    }

}