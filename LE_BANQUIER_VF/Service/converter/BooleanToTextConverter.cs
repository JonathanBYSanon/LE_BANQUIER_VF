using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LE_BANQUIER_VF.Service.converter
{
    public class BooleanToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isOfferRound && isOfferRound)
            {
               
                if (GameProgressService.Instance.Round == 24)
                    return "Après cette malette, ce sera la Dernière Offre !!!";

                    return "L'offre arrive après cette malette !!!";
            }
            if (GameProgressService.Instance.Round == 0)
                return "Choisis ta malette !!!";

            if(GameProgressService.Instance.Round == 25)
                return "Echange ou garde ta malette !!!";

            return $"La prochaine offre arrive aprés : {GameProgressService.Instance.NextOfferRound - GameProgressService.Instance.Round+1} malettes"; // Retourne une chaîne vide si ce n'est pas un round spécial
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
