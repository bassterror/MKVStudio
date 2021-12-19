using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddFilesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly object _collectionParent;
        private readonly IExternalLibrariesService _exLib;

        public AddFilesCommand(object collectionParent, IExternalLibrariesService exLib)
        {
            _collectionParent = collectionParent;
            _exLib = exLib;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_collectionParent is MultiplexerVM multiplexer)
            {
                foreach (string filename in _exLib.Util.GetFileDialog("Video files (*.mkv, *.mp4)|*.mkv;*.mp4|All files (*.*)|*.*", true).FileNames)
                {
                    MultiplexVM multiplex = new(multiplexer, filename, _exLib);
                    multiplexer.Multiplexes.Add(multiplex);
                }
            }
            if (_collectionParent is InputVM input)
            {
                foreach (string fileName in _exLib.Util.GetFileDialog("Video files (*.mkv, *.mp4)|*.mkv;*.mp4|All files (*.*)|*.*", true).FileNames)
                {
                    SourceFileVM sourceFile = new(input, fileName, false);
                    input.SourceFiles.Add(sourceFile);
                    input.CreateTracks(sourceFile);
                }
            }
        }
    }
}
