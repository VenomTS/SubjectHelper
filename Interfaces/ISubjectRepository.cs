using System.Collections.Generic;
using SubjectHelper.Models;

namespace SubjectHelper.Interfaces;

public interface ISubjectRepository
{
    IEnumerable<Subject> GetSubjects();
    Subject? GetSubject(string name);
    Subject? AddSubject(Subject subject);
    Subject? RemoveSubject(Subject subject);
}