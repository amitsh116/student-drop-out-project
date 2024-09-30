using Avalonia.Data.Converters;
using System.Globalization;
using System;

namespace RecommendationGUI.Utils;

/// <summary>
/// Used for determining half of a window's width
/// (min size for recommendations view, to prevent charts messing up).
/// </summary>
public class HalfWidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double windowWidth)
            return windowWidth / 2;
        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double haldWidth)
            return haldWidth * 2;
        return 0;
    }
}
