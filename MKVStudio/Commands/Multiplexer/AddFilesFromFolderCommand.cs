using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddFilesFromFolderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly object _collectionParent;
        private readonly IExternalLibrariesService _exLib;

        public AddFilesFromFolderCommand(object collectionParent, IExternalLibrariesService exLib)
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
                foreach (string filename in _exLib.Util.GetFilesFromFolder("*.mkv|*.mp4"))
                {
                    MultiplexVM multiplex = new(multiplexer, filename, _exLib);
                    multiplexer.Multiplexes.Add(multiplex);
                }
            }
            if (_collectionParent is InputVM input)
            {
                foreach (string filename in _exLib.Util.GetFilesFromFolder("*.mkv|*.mp4"))
                {
                    SourceFileVM sourceFile = new(input, filename, false);
                    input.SourceFiles.Add(sourceFile);
                    input.CreateTracks(sourceFile);
                }
            }
        }
    }
}
