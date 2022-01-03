using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class AddFilesFromFolderCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    private readonly object _collectionParent;
    private readonly IUtilitiesService _util;

    public AddFilesFromFolderCommand(object collectionParent, IUtilitiesService util)
    {
        _collectionParent = collectionParent;
        _util = util;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (_collectionParent is MultiplexerVM multiplexer)
        {
            foreach (string filename in _util.GetFilesFromFolder(_util.SupportedFileTypesCollection.CreateFiltersAllSuportedOnlyExt()))
            {
                MultiplexVM multiplex = new(multiplexer, filename, _util);
                multiplexer.Multiplexes.Add(multiplex);
            }
        }
        if (_collectionParent is InputVM input)
        {
            foreach (string filename in _util.GetFilesFromFolder(_util.SupportedFileTypesCollection.CreateFiltersAllSuportedOnlyExt()))
            {
                SourceFileVM sourceFile = new(filename, false, input);
                input.SourceFiles.Add(sourceFile);
                input.CreateTracks(sourceFile);
            }
        }
    }
}
