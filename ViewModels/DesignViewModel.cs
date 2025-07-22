using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SubjectHelper.ViewModels;

public partial class DesignViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title;
}