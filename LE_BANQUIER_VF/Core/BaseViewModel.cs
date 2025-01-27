using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LE_BANQUIER_VF.Core
{
    /// <summary>
    /// Base class for all view models, impelementing INotifyPropertyChanged
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
