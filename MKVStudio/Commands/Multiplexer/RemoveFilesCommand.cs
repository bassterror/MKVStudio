using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveFilesCommand : ICommand
    {
        private readonly object _collectionParent;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveFilesCommand(object collectionParent)
        {
            _collectionParent = collectionParent;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_collectionParent is MultiplexerVM multiplexer)
            {
                if (parameter is MultiplexVM multiplex)
                {
                    multiplexer.Multiplexes.Remove(multiplex);
                }
            }
            if (_collectionParent is InputVM input)
            {
                if (parameter is SourceFileVM sourceFile)
                {
                    input.SourceFiles.Remove(sourceFile);
                }
            }
        }
    }
}
