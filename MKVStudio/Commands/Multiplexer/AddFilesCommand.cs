using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class AddFilesCommand : BaseCommand
{
    private readonly object _collectionParent;
    private readonly IUtilitiesService _util;

    public AddFilesCommand(object collectionParent, IUtilitiesService util)
    {
        _collectionParent = collectionParent;
        _util = util;
    }

    public override void Execute(object parameter)
    {
        if (_collectionParent is MultiplexerVM multiplexer)
        {
            foreach (string filename in _util.GetFileDialog(_util.SupportedFileTypesCollection.CreateFiltersAllSuported(), true).FileNames)
            {
                MultiplexVM multiplex = new(multiplexer, filename, _util);
                multiplexer.Multiplexes.Add(multiplex);
            }
        }
        if (_collectionParent is InputVM input)
        {
            foreach (string fileName in _util.GetFileDialog(_util.SupportedFileTypesCollection.CreateFiltersAllSuported(), true).FileNames)
            {
                SourceFileVM sourceFile = new(fileName, false, input);
                input.SourceFiles.Add(sourceFile);
                input.CreateTracks(sourceFile);
            }
        }
        if (_collectionParent is AttachmentsVM attachments)
        {
            foreach (string fileName in _util.GetFileDialog(_util.SupportedFileTypesCollection.CreateFiltersAllAttachments(), true).FileNames)
            {
                SourceFileVM sourceFile = new(fileName, false);
                Attachment attachment = new(_util, sourceFile,attachments);
                attachments.NewAttachments.Add(attachment);
            }
        }
    }
}
