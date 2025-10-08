using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Inai.App.Converters;

public class BoolToColorConverter : IValueConverter
{
    public Color UserColor { get; set; } = Colors.LightBlue;
    public Color AIColor { get; set; } = Colors.LightGray;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? AIColor : UserColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
