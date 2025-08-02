using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SubjectHelper.Helper;

namespace SubjectHelper.Converters;

public class AbsenceTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not AbsenceTypes type) return null;
        return type switch
        {
            AbsenceTypes.Lecture => "Lecture",
            AbsenceTypes.Tutorial => "Tutorial",
            AbsenceTypes.Lab => "Lab",
            _ => "Unknown"
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string type) return null;
        return type switch
        {
            "Lecture" => AbsenceTypes.Lecture,
            "Tutorial" => AbsenceTypes.Tutorial,
            "Lab" => AbsenceTypes.Lab,
            _ => null,
        };
    }
}