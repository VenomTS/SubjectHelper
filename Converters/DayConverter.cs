using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace SubjectHelper.Converters;

public class DayConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DayOfWeek day) return null;

        return day switch
        {
            DayOfWeek.Monday => "Monday",
            DayOfWeek.Tuesday => "Tuesday",
            DayOfWeek.Wednesday => "Wednesday",
            DayOfWeek.Thursday => "Thursday",
            DayOfWeek.Friday => "Friday",
            DayOfWeek.Saturday => "Saturday",
            DayOfWeek.Sunday => "Sunday",
            _ => null,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string day)
            return GetDayOfWeek(day);

        if (value?.GetType() == typeof(ComboBoxItem))
        {
            var cbItem = (ComboBoxItem)value;
            return GetDayOfWeek(cbItem?.Content?.ToString());
        }

        return null;
    }

    private DayOfWeek? GetDayOfWeek(string? day)
    {
        return day switch
        {
            "Monday" => DayOfWeek.Monday,
            "Tuesday" => DayOfWeek.Tuesday,
            "Wednesday" => DayOfWeek.Wednesday,
            "Thursday" => DayOfWeek.Thursday,
            "Friday" => DayOfWeek.Friday,
            "Saturday" => DayOfWeek.Saturday,
            "Sunday" => DayOfWeek.Sunday,
            _ => null,
        };
    }
}