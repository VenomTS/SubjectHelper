using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using SubjectHelper.Components.EvaluationForm;
using SubjectHelper.Interfaces;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.Models;
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

    public Task<DialogResult> ShowSubjectForm(string header, Subject? subject)
    {
        throw new NotImplementedException();
    }

    public async Task<DialogResult> ShowEvaluationForm(string header, EvaluationFormViewModel vm)
    {
        vm.Header = header;
        return await Dialog.ShowCustomModal<EvaluationFormView, EvaluationFormViewModel, DialogResult>(vm, _rootWindow, DefaultDialogOptions);
    }
}