using System;
using Avalonia.Media;

namespace SubjectHelper.Helper;

public class RandomColorGenerator
{
    private static Random _random = new Random();
    
    private static double GoldenRatio = 1.61803398874989484820458683436;
    
    public static Color GenerateRandomColor()
    {
        return GenerateGoldenRatioColor();
    }

    private static Color GenerateHsvColor()
    {
        var h = Math.Floor(_random.NextSingle() * 360);
        var s = 70 + Math.Floor(_random.NextSingle() * 30);
        var v = 40 + Math.Floor(_random.NextSingle() * 30);

        s /= 100;
        v /= 100;
        
        return HsvColor.ToRgb(h, s, v);
    }

    private static Color GenerateGoldenRatioColor()
    {
        var h = _random.Next(1, 1024) * GoldenRatio * 360 % 360;
        var s = 0.75;
        var v = 0.6;
        
        return HsvColor.ToRgb(h, s, v);
    }
}