using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SubjectHelper.Converters;

public class TimeSpanTimeOnlyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeOnly time) return null;

        return new TimeSpan(time.Ticks);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeSpan time) return null;

        return new TimeOnly(time.Ticks);
    }
}