using LE_BANQUIER_VF.Core;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing a player in the game
    /// </summary>
    public class Player : BaseViewModel
    {
        // The player's name
        private string _name;

        // The player's choosen briefcase
        private Briefcase _briefcase;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        } 
        public Briefcase Briefcase
        {
            get { return _briefcase; }
            set
            {
                _briefcase = value;
                OnPropertyChanged();
            }
        }
    }
}
