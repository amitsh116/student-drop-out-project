using RecommendationGUI.ViewModels;
using SkiaSharp;
using System;

namespace RecommendationGUI.Utils;

/// <summary>
/// Static class holding color-related utilities.
/// </summary>
public static class ColorUtils
{
    private static readonly Random _random = new();

    /// <summary>
    /// Generates random color for a chart series.
    /// </summary>
    /// <param name="background">
    /// Background color (to prevent generating similar colors), `null` if irrelevant.
    /// </param>
    /// <param name="minDifference">
    /// Minimum allowed color difference from the background (optional, defaults to 100).
    /// </param>
    /// <returns>Random color for a chart series.</returns>
    public static SKColor GetRandomChartColor(SKColor? background = null, int minDifference = 100)
    {
        SKColor color = new(
            (byte)_random.Next(0, 256),
            (byte)_random.Next(0, 256),
            (byte)_random.Next(0, 256)
        );
        if (background is null)
            return color;

        while (GetColorDifference(color, (SKColor)background) < minDifference)
        {
            color = new(
                (byte)_random.Next(0, 256),
                (byte)_random.Next(0, 256),
                (byte)_random.Next(0, 256)
            );
        }

        return color;
    }

    /// <summary>
    /// Calculates the Euclidean distance between two colors in RGB space.
    /// </summary>
    /// <param name="c1">First color.</param>
    /// <param name="c2">Second color.</param>
    /// <returns>Distance between the two colors.</returns>
    private static double GetColorDifference(SKColor c1, SKColor c2)
    {
        int rDiff = c1.Red - c2.Red;
        int gDiff = c1.Green - c2.Green;
        int bDiff = c1.Blue - c2.Blue;
        return Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
    }
}
