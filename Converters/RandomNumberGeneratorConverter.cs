using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace SubjectHelper.Converters;

public class RandomNumberGeneratorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var random = new Random();

        // Min is inclusive, max is exclusive
        var minValue = 0;
        var maxValue = 256;

        var red = System.Convert.ToByte(random.Next(minValue, maxValue));
        var green = System.Convert.ToByte(random.Next(minValue, maxValue));
        var blue = System.Convert.ToByte(random.Next(minValue, maxValue));
        
        var color = new Color(255, red, green, blue); 

        return new SolidColorBrush(color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}