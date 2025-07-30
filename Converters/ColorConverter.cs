using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace SubjectHelper.Converters;

public class ColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not Color color ? null : new SolidColorBrush(color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not SolidColorBrush colorBrush ? null : colorBrush.Color;
    }
}