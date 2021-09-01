using MKVStudio.Commands;
using MKVStudio.ViewModels.Base;
using System.Windows.Input;

namespace MKVStudio.State.MainNavigator
{
    public class MainNavigator : BaseNavigator, IMainNavigator
    {
        public BaseMainViewModel CurrentMainViewModel { get; set; }

        public ICommand UpdateCurrentMainViewModelCommand => new UpdateCurrentMainViewModelCommand(this);
    }
}
