using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.State
{
    [AddINotifyPropertyChangedInterface]
    public class BaseNavigator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
