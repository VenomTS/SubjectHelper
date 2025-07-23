using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using SubjectHelper.Helper;

namespace SubjectHelper.Converters;

public class ProgressBarColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int colorValue) return null;
        var color = IntegerToColorInterpolator.GetColor(colorValue);

        return new SolidColorBrush(color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}