using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace _16_09_2022
{
    public class ColorConvertor : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
            => new SolidColorBrush(Color.FromRgb(System.Convert.ToByte(value[0]), System.Convert.ToByte(value[1]), System.Convert.ToByte(value[2])));

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException("Going back isn't supported");
        
    }
}
