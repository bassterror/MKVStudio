using MKVStudio.Commands;
using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public class MainNavigator : BaseNavigator, IMainNavigator
    {
        public BaseViewModel CurrentMainViewModel { get; set; }

        public ICommand UpdateCurrentMainViewModelCommand => new UpdateCurrentMainViewModelCommand(this);
    }
}
