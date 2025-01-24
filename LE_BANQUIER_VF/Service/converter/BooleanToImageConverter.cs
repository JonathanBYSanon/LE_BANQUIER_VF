using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LE_BANQUIER_VF.Service.converter
{
    public class BooleanToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;
            if (b == null)
                return null;
            if (b.Value)
                return new BitmapImage(new Uri("pack://application:,,,/Resource/Image/maletteOuverte.png"));
            else
                return new BitmapImage(new Uri("pack://application:,,,/Resource/Image/maletteFerme.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
