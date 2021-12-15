using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateMultiplexTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MultiplexVM _multiplex;

        public UpdateMultiplexTabCommand(MultiplexVM multiplex)
        {
            _multiplex = multiplex;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModel)
            {
                switch (viewModel)
                {
                    case ViewModelTypes.Input:
                        _multiplex.SelectedMultiplexTab = _multiplex.Input;
                        break;
                    case ViewModelTypes.Output:
                        _multiplex.SelectedMultiplexTab = _multiplex.Output;
                        break;
                    case ViewModelTypes.Attachments:
                        _multiplex.SelectedMultiplexTab = _multiplex.Attachments;
                        break;
                    case ViewModelTypes.Chapters:
                        _multiplex.SelectedMultiplexTab = _multiplex.Chapters;
                        break;
                }
            }
        }
    }
}
