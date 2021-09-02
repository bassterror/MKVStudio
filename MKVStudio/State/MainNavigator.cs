using MKVStudio.Commands;
using MKVStudio.ViewModels;
using MKVStudio.ViewModels.Factories;
using System.Windows.Input;

namespace MKVStudio.State
{
    public class MainNavigator : BaseNavigator, IMainNavigator
    {
        public BaseViewModel CurrentViewModel { get; set; }

        public ICommand UpdateCurrentMainViewModelCommand { get; set; }

        public MainNavigator(IRootViewModelFactory viewModelFactory)
        {
            UpdateCurrentMainViewModelCommand = new UpdateCurrentMainViewModelCommand(this, viewModelFactory);
        }
    }
}
