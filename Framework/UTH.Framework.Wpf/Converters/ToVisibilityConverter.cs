using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using UTH.Infrastructure.Utility;

namespace UTH.Framework.Wpf
{
    public class ToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = BoolHelper.Get(value);
            if (StringHelper.Get(parameter) == "inverse")
            {
                return (flag ? Visibility.Collapsed : Visibility.Visible);
            }
            else
            {
                return (flag ? Visibility.Visible : Visibility.Collapsed);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
