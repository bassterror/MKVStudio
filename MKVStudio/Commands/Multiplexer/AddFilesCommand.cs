using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class AddFilesCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    private readonly object _collectionParent;
    private readonly IUtilitiesService _util;

    public AddFilesCommand(object collectionParent, IUtilitiesService util)
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
                AttachmentVM attachment = new(attachments, sourceFile, _util);
                attachments.NewAttachments.Add(attachment);
            }
        }
    }
}
