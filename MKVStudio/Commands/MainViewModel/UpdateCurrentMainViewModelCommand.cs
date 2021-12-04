using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentMainViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainViewModel _main;
        private readonly FilesViewModel _files;
        private readonly QueueViewModel _queue;

        public UpdateCurrentMainViewModelCommand(MainViewModel mainViewModel, FilesViewModel filesViewModel, QueueViewModel queueViewModel)
        {
            _main = mainViewModel;
            _files = filesViewModel;
            _queue = queueViewModel;
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
                        _main.CurrentMainViewModel = _files;
                        break;
                    case ViewModelTypes.Queue:
                        _main.CurrentMainViewModel = _queue;
                        break;
                    default:
                        throw new ArgumentException("No such Main View Model");
                }
            }
        }
    }
}
