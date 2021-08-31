using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseMainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
