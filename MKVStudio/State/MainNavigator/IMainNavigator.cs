using MKVStudio.ViewModels.Base;
using System.Windows.Input;

namespace MKVStudio.State.MainNavigator
{
    public enum MainViewModelType
    {
        Files,
        Queue
    }

    public interface IMainNavigator
    {
        BaseMainViewModel CurrentMainViewModel { get; set; }
        ICommand UpdateCurrentMainViewModelCommand { get; }
    }
}
