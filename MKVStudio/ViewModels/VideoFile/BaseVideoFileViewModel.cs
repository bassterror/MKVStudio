using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.ViewModels.VideoFile
{
    [AddINotifyPropertyChangedInterface]
    public class BaseVideoFileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
