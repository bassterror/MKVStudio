using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class BrowseCommand : ICommand
    {
        private readonly MultiplexVM _multiplex;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public BrowseCommand(MultiplexVM multiplex, IExternalLibrariesService exLib)
        {
            _multiplex = multiplex;
            _exLib = exLib;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // Set new output dir
            //_multiplex.OutputPath = _exLib.Util.GetFolder();
        }
    }
}
