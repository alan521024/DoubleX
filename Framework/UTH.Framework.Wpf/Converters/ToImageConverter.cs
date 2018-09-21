using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Globalization;

namespace UTH.Framework.Wpf
{
    public class ToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                return new BitmapImage(new Uri(path, UriKind.Absolute));
            }
            else
            {
                return null;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
