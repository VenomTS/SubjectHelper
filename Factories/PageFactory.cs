using System;
using SubjectHelper.Helper;
using SubjectHelper.ViewModels;
using SubjectHelper.ViewModels.Bases;

namespace SubjectHelper.Factories;

public class PageFactory
{
    private readonly Func<ApplicationPages, object?, PageViewModel> _factory;

    public PageFactory(Func<ApplicationPages, object?, PageViewModel> factory)
    {
        _factory = factory;
    }
    
    public PageViewModel GetPageViewModel(ApplicationPages page, object? data = null)
    {
        return _factory.Invoke(page, data);
    }
}