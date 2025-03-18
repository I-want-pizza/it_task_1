using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace roman_paper_task_1.Converters;

public class AuthorsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IReadOnlyList<string> authors)
        {
            return string.Join(", ", authors);
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}