using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public interface IVideoFileNavigator
    {
        BaseViewModel CurrentVideoFileViewModel { get; set; }
        ICommand UpdateCurrentVideoFileViewModelCommand { get; }
    }
}
