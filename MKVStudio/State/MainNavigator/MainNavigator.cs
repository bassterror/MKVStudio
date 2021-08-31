using MKVStudio.Commands;
using MKVStudio.ViewModels.Base;
using System.Windows.Input;

namespace MKVStudio.State.MainNavigator
{
    public class MainNavigator : IMainNavigator
    {
        public BaseMainViewModel CurrentViewModel { get; set; }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
