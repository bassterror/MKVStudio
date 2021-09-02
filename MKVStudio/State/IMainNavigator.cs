using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public interface IMainNavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentMainViewModelCommand { get; }
    }
}
