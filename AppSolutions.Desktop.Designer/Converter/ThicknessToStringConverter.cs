using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AppSolutions.Desktop.Designer.Converter
{
    public class ThicknessToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Thickness)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return new Thickness(Double.Parse(value.ToString().Split(',')[0]), Double.Parse(value.ToString().Split(',')[1]), Double.Parse(value.ToString().Split(',')[2]), Double.Parse(value.ToString().Split(',')[3]));
            }
            catch
            {
                MessageBox.Show("Please, format the input value like this: \"number,number,number,number\".");
                return new Thickness(0);
            }
        }
    }
}
