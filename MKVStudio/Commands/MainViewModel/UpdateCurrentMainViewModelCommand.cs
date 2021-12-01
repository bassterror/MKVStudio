using MKVStudio.State;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentMainViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly FilesViewModel _filesViewModel;
        private readonly QueueViewModel _queueViewModel;

        public UpdateCurrentMainViewModelCommand(INavigator navigator, FilesViewModel filesViewModel, QueueViewModel queueViewModel)
        {
            _navigator = navigator;
            _filesViewModel = filesViewModel;
            _queueViewModel = queueViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModelType)
            {
                switch (viewModelType)
                {
                    case ViewModelTypes.Files:
                        _navigator.CurrentMainViewModel = _filesViewModel;
                        break;
                    case ViewModelTypes.Queue:
                        _navigator.CurrentMainViewModel = _queueViewModel;
                        break;
                    default:
                        throw new ArgumentException("No such Main View Model");
                }
            }
        }
    }
}
