using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.ViewModels;

[AddINotifyPropertyChangedInterface]
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
}
