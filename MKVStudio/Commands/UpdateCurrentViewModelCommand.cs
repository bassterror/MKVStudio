using MKVStudio.State.MainNavigator;
using MKVStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IMainNavigator _navigator;

        public UpdateCurrentViewModelCommand(IMainNavigator navigator)
        {
            _navigator = navigator;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is MainViewModelType viewModelType)
            {
                switch (viewModelType)
                {
                    case MainViewModelType.Video:
                        _navigator.CurrentViewModel = new VideosViewModel(string.Empty);
                        break;
                    case MainViewModelType.Queue:
                        _navigator.CurrentViewModel = new QueueViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
