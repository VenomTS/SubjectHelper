using System;
using System.Collections.Generic;
using System.Linq;
using SubjectHelper.Interfaces.Services;
using SubjectHelper.ViewModels.ScheduleMaker;

namespace SubjectHelper.Services;

public class ScheduleMakingService : IScheduleMakingService
{
    private List<List<SMSectionViewModel>> _allSchedules = [];

    private SMSubjectViewModel? _impossibleSubject;

    private int _requiredSubjectCount;
    
    public (SMSubjectViewModel?, List<List<SMSectionViewModel>>?) GenerateAllSchedules(List<SMSubjectViewModel> subjects)
    {
        _allSchedules = [];
        _impossibleSubject = null;

        _requiredSubjectCount = subjects.Count;
        
        GenerateSchedules(subjects, []);
        
        if(_impossibleSubject != null)
            return (_impossibleSubject, null);

        return (null, _allSchedules);
    }

    private void GenerateSchedules(List<SMSubjectViewModel> subjects, List<SMSectionViewModel> selectedSections)
    {
        if (subjects.Count == 0)
        {
            if (selectedSections.Count != _requiredSubjectCount) return;
            _allSchedules.Add([..selectedSections]);
            return;
        }

        var currentSubject = subjects[^1];
        subjects.RemoveAt(subjects.Count - 1);

        var anySectionSelected = false;

        foreach (var currentSection in currentSubject.Sections)
        {
            var canSectionFit = selectedSections.ToList().All(selectedSection => !IsSectionOverlap(currentSection, selectedSection));

            if (!canSectionFit) continue;

            anySectionSelected = true;
            selectedSections.Add(currentSection);
            GenerateSchedules([..subjects], selectedSections);
            selectedSections.Remove(currentSection);
        }

        if (!anySectionSelected)
            _impossibleSubject = currentSubject;
    }

    private bool IsSectionOverlap(SMSectionViewModel sectionA, SMSectionViewModel sectionB)
    {
        var timesA = sectionA.Times;
        var timesB = sectionB.Times;

        foreach (var timeA in timesA)
        {
            var dayA = timeA.Day;
            var startA = timeA.StartTime;
            var endA = timeA.EndTime;
            foreach (var timeB in timesB)
            {
                var dayB = timeB.Day;
                var startB = timeB.StartTime;
                var endB = timeB.EndTime;
                if (dayA != dayB) continue;
                
                /*
                 * There is no overlap if (A < B) or (B < A)
                 * |----- Time A -----|
                 *                      |----- Time B -----|
                 * OR
                 * |----- Time B -----|
                 *                      |----- Time A -----|
                 */

                if (endA < startB || endB < startA) continue;
                return true;
            }
        }
        return false;
    }
}