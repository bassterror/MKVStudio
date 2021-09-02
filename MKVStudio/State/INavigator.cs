using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public interface INavigator
    {
        BaseViewModel CurrentMainViewModel { get; set; }
        BaseViewModel CurrentFilesViewModel { get; set; }
        BaseViewModel CurrentVideoFileViewModel { get; set; }
        ICommand UpdateCurrentMainViewModelCommand { get; }
        ICommand UpdateCurrentFilesViewModelCommand { get; }
        ICommand UpdateCurrentVideoFileViewModelCommand { get; }
    }
}
