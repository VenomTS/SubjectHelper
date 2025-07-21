using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SubjectHelper.Converters;

public class CountToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is > 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Don't think this needs to be implemented?
        throw new NotImplementedException();
    }
}