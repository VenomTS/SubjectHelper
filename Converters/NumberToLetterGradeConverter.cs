using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SubjectHelper.Helper;

namespace SubjectHelper.Converters;

public class NumberToLetterGradeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int grade) return null;

        var letterGrade = GradeManipulator.GetGrade(grade);
        
        return $"{grade} ({letterGrade})";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string letterGrade) return null;
        
        var grade = GradeManipulator.GetGrade(letterGrade);

        return $"{grade} ({letterGrade})";
    }
}