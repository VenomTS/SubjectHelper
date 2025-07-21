using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SubjectHelper.Converters;

public class PercentageFormatConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value + "%";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is string str && str.EndsWith('%'))
            return str.TrimEnd('%');
        return value;
    }
}