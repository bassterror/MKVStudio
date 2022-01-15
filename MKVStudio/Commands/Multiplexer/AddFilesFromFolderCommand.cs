using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class AddFilesFromFolderCommand : BaseCommand
{
    private readonly object _collectionParent;
    private readonly IUtilitiesService _util;

    public AddFilesFromFolderCommand(object collectionParent, IUtilitiesService util)
    {
        _collectionParent = collectionParent;
        _util = util;
    }

    public override void Execute(object parameter)
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
                SourceFileInfo sourceFile = new(_util, filename, false);
                input.SourceFiles.Add(sourceFile);
                input.CreateTracks(sourceFile);
            }
        }
    }
}
