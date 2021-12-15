using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddFilesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MultiplexerVM _multiplexer;
        private readonly IExternalLibrariesService _exLib;

        public AddFilesCommand(MultiplexerVM multiplexer, IExternalLibrariesService exLib)
        {
            _multiplexer = multiplexer;
            _exLib = exLib;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (string filename in _exLib.Util.GetFileDialog("Video files (*.mkv, *.mp4)|*.mkv;*.mp4|All files (*.*)|*.*", true).FileNames)
            {
                MultiplexVM multiplex = new(_multiplexer, filename, _exLib);
                _multiplexer.Multiplexes.Add(multiplex);
            }
        }
    }
}
