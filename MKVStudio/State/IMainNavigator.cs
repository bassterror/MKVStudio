using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public enum MainViewModelType
    {
        Files,
        Queue
    }

    public interface IMainNavigator
    {
        BaseViewModel CurrentMainViewModel { get; set; }
        ICommand UpdateCurrentMainViewModelCommand { get; }
    }
}
