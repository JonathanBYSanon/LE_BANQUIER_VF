using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LE_BANQUIER_VF.Service.converter
{
    /// <summary>
    /// Converter of boolean to text for the next offer in the progress bar
    /// </summary>
    public class BooleanToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isOfferRound && isOfferRound)
            {
               
                if (GameProgressService.Instance.Round == 24)
                    return "Dernier tour avec offre";

                    return "Tour avec offre";
            }
            if (GameProgressService.Instance.Round == 0)
                return "Choisis ta malette !!!";

            if(GameProgressService.Instance.Round == 25)
                return "Echange ou garde ta malette";

            return $"Offre aprés : {GameProgressService.Instance.NextOfferRound - GameProgressService.Instance.Round+1} malettes";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
