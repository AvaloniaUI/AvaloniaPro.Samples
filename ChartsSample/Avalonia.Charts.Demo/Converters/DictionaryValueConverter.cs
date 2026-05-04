using System;
using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.Charts.Demo.Converters;

public sealed class DictionaryValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IDictionary dictionary || parameter is not string key)
        {
            return string.Empty;
        }

        return dictionary.Contains(key) ? dictionary[key]?.ToString() ?? string.Empty : string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
