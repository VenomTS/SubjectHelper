using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SubjectHelper.Data;
using SubjectHelper.Factories;
using SubjectHelper.Helper;
using SubjectHelper.Interfaces;
using SubjectHelper.Interfaces.Repositories;
using SubjectHelper.Interfaces.ScheduleMaker;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.Repositories;
using SubjectHelper.Repositories.ScheduleMaker;
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

        collection.AddDbContext<DatabaseContext>();

        // Singleton = Always in Memory
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageFactory>();
        collection.AddSingleton<INavigationService, NavigationService>();
        collection.AddSingleton<IDialogService, DialogService>();
        collection.AddSingleton<IToastService, ToastService>();
        
        // Scoped = Created once and reused
        collection.AddScoped<SubjectsListViewModel>();
        collection.AddScoped<ScheduleMakerViewModel>();
        collection.AddScoped<ISubjectRepository, SubjectRepository>();
        collection.AddScoped<IEvaluationRepository, EvaluationRepository>();
        collection.AddScoped<IAbsenceRepository, AbsenceRepository>();
        collection.AddScoped<IScheduleMakerRepository, ScheduleMakerRepository>();
        
        // Transient - Created every time
        // MUST BE TRANSIENT - IF NOT ISSUES MAY HAPPEN
        collection.AddTransient<SubjectViewModel>();

        collection.AddSingleton<Func<ApplicationPages, object?, PageViewModel>>(provider => (name, data) =>
        {
            switch (name)
            {
                case ApplicationPages.Subjects:
                    return provider.GetRequiredService<SubjectsListViewModel>();
                case ApplicationPages.Subject:
                    if (data is not int subjectId)
                        throw new Exception("Data passed MUST BE SUBJECT'S ID");
                    var vm = provider.GetRequiredService<SubjectViewModel>();
                    _ = vm.Initialize(subjectId);
                    return vm;
                case ApplicationPages.ScheduleMakerSubjects:
                    return provider.GetRequiredService<ScheduleMakerViewModel>();
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

            // Initializing "Window"
            services.GetRequiredService<IDialogService>().Initialize(desktop.MainWindow);
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