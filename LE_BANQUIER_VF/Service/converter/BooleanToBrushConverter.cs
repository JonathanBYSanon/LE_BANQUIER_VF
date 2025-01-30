using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace LE_BANQUIER_VF.Service.converter
{

    public class BooleanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEnabled)
            {
                return isEnabled ? Brushes.DarkGoldenrod : Brushes.Transparent; // Activé = Or, Désactivé = Transparent
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
