using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;

namespace SubjectHelper.Helper;

public class IntegerToColorInterpolator
{
    private static readonly Color FColor = Color.FromRgb(244, 67, 54);
    private static readonly Color CColor = Color.FromRgb(255, 152, 0);
    private static readonly Color BColor = Color.FromRgb(33, 150, 243);
    private static readonly Color AColor = Color.FromRgb(76, 175, 80);

    private const int FLimit = 55;
    private const int CLimit = 70;
    private const int BLimit= 85;
    private const int ALimit = 100;
    
    public static Color GetColor(int value)
    {
        var colors = GetColors(value).ToList();
        var startColor = colors[0];
        var endColor = colors[1];
        var fromR = startColor.R;
        var fromG = startColor.G;
        var fromB = startColor.B;
        var toR = endColor.R;
        var toG = endColor.G;
        var toB = endColor.B;

        var bias = CalculateBias(value);

        var resultR = Lerp(fromR, toR, bias);
        var resultG = Lerp(fromG, toG, bias);
        var resultB = Lerp(fromB, toB, bias);
        
        var resultRByte = Convert.ToByte(resultR);
        var resultGByte = Convert.ToByte(resultG);
        var resultBByte = Convert.ToByte(resultB);

        return new Color(255, resultRByte, resultGByte, resultBByte);
    }

    private static float CalculateBias(int value)
    {
        return value switch
        {
            < FLimit => value / (FLimit * 1f),
            < CLimit => (value - FLimit) / (CLimit - FLimit * 1f),
            < BLimit => (value - CLimit) / (BLimit - CLimit * 1f),
            _ => (value - BLimit) / (ALimit - BLimit * 1f)
        };
    }

    private static IEnumerable<Color> GetColors(int value)
    {
        return value switch
        {
            < FLimit => new List<Color>([FColor, CColor]),
            < CLimit => new List<Color>([CColor, BColor]),
            < BLimit => new List<Color>([BColor, AColor]),
            _ => new List<Color>([AColor, AColor])
        };
    }


    private static float Lerp(int v1, int v2, float t)
    {
        return (1 - t) * v1 + t * v2; 
    }
}