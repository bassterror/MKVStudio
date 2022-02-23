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
            AddFilesToMultiplexer(multiplexer);
        }
        if (_collectionParent is InputVM input)
        {
            AddFilesToInput(input);
        }
        if (_collectionParent is AttachmentsVM attachments)
        {
            AddFilesToAttachments(attachments);
        }
    }

    private void AddFilesToMultiplexer(MultiplexerVM multiplexer)
    {
        string filter = _util.Preferences.SupportedFileTypes.CreateFiltersAllSuported();
        string[] fileNames = _util.GetFileDialog(filter, true).FileNames;
        foreach (string filename in fileNames)
        {
            SourceFileInfo sourceFile = new(_util, filename, true);
            MultiplexVM multiplex = new(_util, multiplexer, sourceFile);
            multiplexer.Multiplexes.Add(multiplex);
        }
    }

    private void AddFilesToInput(InputVM input)
    {
        string filter = _util.Preferences.SupportedFileTypes.CreateFiltersAllSuported();
        string[] fileNames = _util.GetFileDialog(filter, true).FileNames;
        foreach (string fileName in fileNames)
        {
            SourceFileInfo sourceFile = new(_util, fileName, false);
            input.SourceFiles.Add(sourceFile);
            input.CreateTracks(sourceFile);
        }
    }

    private void AddFilesToAttachments(AttachmentsVM attachments)
    {
        string filter = _util.Preferences.SupportedFileTypes.CreateFiltersAllAttachments();
        string[] fileNames = _util.GetFileDialog(filter, true).FileNames;
        foreach (string fileName in fileNames)
        {
            SourceFileInfo sourceFile = new(_util, fileName, false);
            Attachment attachment = new(_util, sourceFile, attachments);
            attachments.NewAttachments.Add(attachment);
        }
    }
}
