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
        string filter = _util.Settings.SupportedFileTypes.CreateFiltersAllSuportedOnlyExt();
        string[] fileNames = _util.GetFilesFromFolder(filter);
        if (_collectionParent is MultiplexerVM multiplexer)
        {
            AddFilesToMultiplexer(multiplexer, fileNames);
        }
        if (_collectionParent is InputVM input)
        {
            AddFilesToInput(input, fileNames);
        }
    }

    private void AddFilesToMultiplexer(MultiplexerVM multiplexer, string[] fileNames)
    {
        foreach (string filename in fileNames)
        {
            SourceFileInfo sourceFile = new(_util, filename, true);
            MultiplexVM multiplex = new(_util, multiplexer, sourceFile);
            multiplexer.Multiplexes.Add(multiplex);
        }
    }

    private void AddFilesToInput(InputVM input, string[] fileNames)
    {
        foreach (string filename in fileNames)
        {
            SourceFileInfo sourceFile = new(_util, filename, false);
            input.SourceFiles.Add(sourceFile);
            input.CreateTracks(sourceFile);
        }
    }
}
