using System.Threading.Tasks;
using Avalonia.Controls;
using SubjectHelper.Components.EvaluationForm;
using SubjectHelper.Models;
using Ursa.Controls;

namespace SubjectHelper.Interfaces;

public interface IDialogService
{
    void Initialize(Window rootWindow);
    
    Task<DialogResult> ShowSubjectForm(string header, Subject? subject);

    Task<DialogResult> ShowEvaluationForm(string header, EvaluationFormViewModel vm);
}