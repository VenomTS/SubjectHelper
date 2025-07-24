using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SubjectHelper.Factories;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.Repositories;
using SubjectHelper.Services;
using SubjectHelper.ViewModels;
using SubjectHelper.ViewModels.Bases;
using SubjectHelper.Views;

namespace SubjectHelper;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();

        // Singleton = Always in Memory
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageFactory>();
        collection.AddSingleton<INavigationService, NavigationService>();
        
        // Scoped = Created once and reused
        collection.AddScoped<SubjectsListViewModel>();
        collection.AddScoped<ISubjectRepository, SubjectRepository>();
        
        // Transient = Created every time
        collection.AddTransient<SubjectViewModel>();

        collection.AddSingleton<Func<ApplicationPages, object?, PageViewModel>>(provider => (name, data) =>
        {
            switch (name)
            {
                case ApplicationPages.Subjects:
                    return provider.GetRequiredService<SubjectsListViewModel>();
                case ApplicationPages.Subject:
                    if (data is not string subjectName)
                        throw new Exception("Data passed MUST BE SUBJECT NAME");
                    var vm = provider.GetRequiredService<SubjectViewModel>();
                    vm.Initialize(subjectName);
                    return vm;
                case ApplicationPages.ScheduleMaker:
                    break;
                case ApplicationPages.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(name), name, null);
            }
            throw new Exception("Unknown ApplicationPage");
        });
        
        var services = collection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}