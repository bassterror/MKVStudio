using MKVStudio.State.MainNavigator;
using MKVStudio.ViewModels.Files;
using MKVStudio.ViewModels.Queue;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentMainViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IMainNavigator _navigator;

        public UpdateCurrentMainViewModelCommand(IMainNavigator navigator)
        {
            _navigator = navigator;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is MainViewModelType mainViewModelType)
            {
                switch (mainViewModelType)
                {
                    case MainViewModelType.Files:
                        _navigator.CurrentMainViewModel = new FilesViewModel();
                        break;
                    case MainViewModelType.Queue:
                        _navigator.CurrentMainViewModel = new QueueViewModel();
                        break;
                    default:
                        throw new ArgumentException("No such MainViewModelType");
                }
            }
        }
    }
}
