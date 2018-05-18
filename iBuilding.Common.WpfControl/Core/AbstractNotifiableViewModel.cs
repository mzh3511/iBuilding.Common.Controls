using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RanOpt.iBuilding.Common.WpfControl.Core
{
    public abstract class AbstractNotifiableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}