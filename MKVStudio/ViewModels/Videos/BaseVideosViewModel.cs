using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.ViewModels.Videos
{
    [AddINotifyPropertyChangedInterface]
    public class BaseVideosViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
