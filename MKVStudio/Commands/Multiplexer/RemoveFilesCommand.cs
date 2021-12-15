using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveFilesCommand : ICommand
    {
        private readonly MultiplexerVM _multiplexer;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveFilesCommand(MultiplexerVM multiplexer)
        {
            _multiplexer = multiplexer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is MultiplexVM multiplex)
            {
                _multiplexer.Multiplexes.Remove(multiplex);
            }
        }
    }
}
