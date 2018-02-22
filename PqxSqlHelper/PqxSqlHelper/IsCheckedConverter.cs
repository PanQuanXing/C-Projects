using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PqxSqlHelper
{
    class IsCheckedConverter:IValueConverter
    {
        //当值从绑定源传播给绑定目标时，调用方法Convert;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is bool) ? (!(bool)value) : value;
        }
        //目标传播给绑定源时，调用此方法ConvertBack;
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
