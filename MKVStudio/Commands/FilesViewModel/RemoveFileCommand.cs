using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MultiplexerVM _multiplexer;
        private readonly MultiplexVM _multiplex;

        public RemoveFileCommand(MultiplexerVM multiplexer, MultiplexVM multiplex)
        {
            _multiplexer = multiplexer;
            _multiplex = multiplex;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _multiplexer.Multiplexes.Remove(_multiplex);
        }
    }
}
