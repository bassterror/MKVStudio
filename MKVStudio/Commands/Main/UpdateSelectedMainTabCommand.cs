using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateSelectedMainTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MainVM _main;
        private readonly MultiplexerVM _multiplexer;
        private readonly JobQueueVM _jobQueue;

        public UpdateSelectedMainTabCommand(MainVM main, MultiplexerVM multiplexer, JobQueueVM jobQueue)
        {
            _main = main;
            _multiplexer = multiplexer;
            _jobQueue = jobQueue;
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
                    case ViewModelTypes.Multiplexer:
                        _main.SelectedMainTab = _multiplexer;
                        break;
                    case ViewModelTypes.JobQueue:
                        _main.SelectedMainTab = _jobQueue;
                        break;
                }
            }
        }
    }
}
