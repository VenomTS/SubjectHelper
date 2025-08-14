using System.Threading.Tasks;
using Avalonia.Controls;
using SubjectHelper.Components.AbsenceForm;
using SubjectHelper.Components.EvaluationForm;
using SubjectHelper.Components.ScheduleMaker.Forms.Subject;
using SubjectHelper.Components.ScheduleMaker.Forms.Timeslot;
using SubjectHelper.Components.SelectionForm;
using SubjectHelper.Components.SubjectForm;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.ViewModels.ScheduleMaker;
using Ursa.Controls;

namespace SubjectHelper.Services;

public class DialogService : IDialogService
{
    private Window? _rootWindow;
    
    private static readonly DialogOptions DefaultDialogOptions = new()
    {
        StartupLocation = WindowStartupLocation.CenterOwner,
        Mode = DialogMode.None,
        IsCloseButtonVisible = false,
        CanResize = false,
    };

    public void Initialize(Window rootWindow)
    {
        _rootWindow = rootWindow;
    }

    public async Task<DialogResult> ShowSubjectForm(string header, SubjectFormViewModel vm)
    {
        vm.Header = header;
        return await Dialog.ShowCustomModal<SubjectFormView, SubjectFormViewModel, DialogResult>(vm, _rootWindow, DefaultDialogOptions);
    }

    public async Task<DialogResult> ShowEvaluationForm(string header, EvaluationFormViewModel vm)
    {
        vm.Header = header;
        return await Dialog.ShowCustomModal<EvaluationFormView, EvaluationFormViewModel, DialogResult>(vm, _rootWindow, DefaultDialogOptions);
    }

    public async Task<DialogResult> ShowAbsenceForm(string header, AbsenceFormViewModel vm)
    {
        vm.Header = header;
        return await Dialog.ShowCustomModal<AbsenceFormView, AbsenceFormViewModel, DialogResult>(vm, _rootWindow, DefaultDialogOptions);
    }

    public async Task<DialogResult> ShowSelectionForm()
    {
        var customDialogOptions = new DialogOptions
        {
            StartupLocation = DefaultDialogOptions.StartupLocation,
            Mode = DefaultDialogOptions.Mode,
            IsCloseButtonVisible = true,
            CanResize = DefaultDialogOptions.CanResize,
        };
        return await Dialog.ShowCustomModal<SelectionFormView, SelectionFormViewModel, DialogResult>(new SelectionFormViewModel(), _rootWindow, customDialogOptions);
    }

    public async Task<DialogResult> ShowSMSubjectForm(string header, SMSubjectFormViewModel vm)
    {
        vm.Header = header;
        return await Dialog.ShowCustomModal<SMSubjectFormView, SMSubjectFormViewModel, DialogResult>(vm, _rootWindow,
            DefaultDialogOptions);
    }

    public async Task<DialogResult> ShowSMTimeForm(string header, SMTimeFormViewModel vm)
    {
        vm.Header = header;
        return await Dialog.ShowCustomModal<SMTimeFormView, SMTimeFormViewModel, DialogResult>(vm, _rootWindow,
            DefaultDialogOptions);
    }
}