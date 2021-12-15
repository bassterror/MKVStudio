using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class TrackProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
