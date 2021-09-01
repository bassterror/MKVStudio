using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.ViewModels.Files
{
    [AddINotifyPropertyChangedInterface]
    public class BaseFilesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
