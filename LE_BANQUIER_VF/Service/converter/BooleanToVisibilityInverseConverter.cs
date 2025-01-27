using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LE_BANQUIER_VF.Service.converter
{
    /// <summary>
    /// Converter to convert a boolean value to a visibility value for the briefcase but inverse
    /// </summary>
    public class BooleanToVisibilityInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;
            if (bValue)
                return System.Windows.Visibility.Collapsed;
            else
                return System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Visibility visibility = (System.Windows.Visibility)value;
            if (visibility == System.Windows.Visibility.Visible)
                return false;
            else
                return true;
        }
    } 
}
