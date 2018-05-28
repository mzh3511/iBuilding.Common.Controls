using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Test.WpfControl.Views
{
    public class Bool2VisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolValue = (bool?)value;
            if (boolValue.HasValue && boolValue.Value)
                return Visibility.Visible;
            return Visibility.Hidden;
        }
    }
}