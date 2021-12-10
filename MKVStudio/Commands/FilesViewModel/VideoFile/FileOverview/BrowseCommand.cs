using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class BrowseCommand : ICommand
    {
        private readonly FileOverviewVM _fileOverview;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public BrowseCommand(FileOverviewVM fileOverviewVM, IExternalLibrariesService exLib)
        {
            _fileOverview = fileOverviewVM;
            _exLib = exLib;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _fileOverview.OutputPath = _exLib.Util.GetFolder();
        }
    }
}
