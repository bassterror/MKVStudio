using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public interface INavigator
    {
        BaseViewModel CurrentMainViewModel { get; set; }
        ICommand UpdateCurrentMainViewModelCommand { get; }
    }
}
