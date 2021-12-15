using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddSourceFilesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddSourceFilesCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
