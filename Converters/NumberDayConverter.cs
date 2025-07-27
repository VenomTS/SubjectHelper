using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SubjectHelper.Converters;

public class NumberDayConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int day) return null;

        return day switch
        {
            0 => "Mon",
            1 => "Tue",
            2 => "Wed",
            3 => "Thu",
            4 => "Fri",
            5 => "Sat",
            6 => "Sun",
            _ => null,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string day) return null;

        return day switch
        {
            "Mon" => 0,
            "Tue" => 1,
            "Wed" => 2,
            "Thu" => 3,
            "Fri" => 4,
            "Sat" => 5,
            "Sun" => 6,
            _ => null,
        };
    }
}