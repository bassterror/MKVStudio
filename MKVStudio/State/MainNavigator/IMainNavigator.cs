using MKVStudio.ViewModels.Base;
using System.Windows.Input;

namespace MKVStudio.State.MainNavigator
{
    public enum MainViewModelType
    {
        Video,
        Queue
    }

    public interface IMainNavigator
    {
        BaseMainViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
