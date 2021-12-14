using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class ClearFilesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MultiplexerVM _multiplexer;

        public ClearFilesCommand(MultiplexerVM multiplexer)
        {
            _multiplexer = multiplexer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _multiplexer.Multiplexes.Clear();
        }
    }
}
