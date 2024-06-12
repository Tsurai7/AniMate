using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace AniMate_app.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isInLikes && isInLikes)
            {
                return Colors.Red;
            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
