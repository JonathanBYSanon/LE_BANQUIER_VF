using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LE_BANQUIER_VF.Model
{
    public class Host : BaseViewModel
    {
        // The messsage the host is saying to the player
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
                SoundManagerService.Instance.PlayNotif("NewMessageSoundEffect.wav");
            }
        }
    }
}
