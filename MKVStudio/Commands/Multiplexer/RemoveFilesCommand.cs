using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MKVStudio.Commands;

public class RemoveFilesCommand : BaseCommand
{
    private readonly object _collectionParent;

    public RemoveFilesCommand(object collectionParent)
    {
        _collectionParent = collectionParent;
    }

    public override void Execute(object parameter)
    {
        if (_collectionParent is MultiplexerVM multiplexer)
        {
            if (parameter is MultiplexVM multiplex)
            {
                _ = multiplexer.Multiplexes.Remove(multiplex);
            }
        }
        if (_collectionParent is InputVM input)
        {
            if (parameter is SourceFileVM sourceFile)
            {
                if (!sourceFile.IsPrimary)
                {
                    _ = input.SourceFiles.Remove(sourceFile);
                    List<TrackVM> tracks = input.Tracks.Where(t => t.SourceFile == sourceFile).ToList();
                    foreach (TrackVM track in tracks)
                    {
                        _ = input.Tracks.Remove(track);
                    }
                }
            }
        }
        if (_collectionParent is AttachmentsVM attachments)
        {
            if (parameter is Attachment attachment)
            {
                _ = attachments.NewAttachments.Remove(attachment);
            }
        }
    }
}
