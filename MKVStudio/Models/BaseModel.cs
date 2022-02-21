using PropertyChanged;
using System.ComponentModel;

namespace MKVStudio.Models;

[AddINotifyPropertyChangedInterface]
public class BaseModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
}
