using System.Threading.Tasks;
using Avalonia.Controls;
using SubjectHelper.Components.AbsenceForm;
using SubjectHelper.Components.EvaluationForm;
using SubjectHelper.Components.SubjectForm;
using SubjectHelper.Models;
using Ursa.Controls;

namespace SubjectHelper.Interfaces.Services;

public interface IDialogService
{
    void Initialize(Window rootWindow);
    
    Task<DialogResult> ShowSubjectForm(string header, SubjectFormViewModel vm);

    Task<DialogResult> ShowEvaluationForm(string header, EvaluationFormViewModel vm);

    Task<DialogResult> ShowAbsenceForm(string header, AbsenceFormViewModel vm);
}