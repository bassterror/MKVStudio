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
            RemoveFilesFromMultiplexer(parameter, multiplexer);
        }
        if (_collectionParent is InputVM input)
        {
            RemoveFilesFromInput(parameter, input);
        }
        if (_collectionParent is AttachmentsVM attachments)
        {
            RemoveFilesFromAttachments(parameter, attachments);
        }
    }

    private static void RemoveFilesFromMultiplexer(object parameter, MultiplexerVM multiplexer)
    {
        if (parameter is MultiplexVM multiplex)
        {
            _ = multiplexer.Multiplexes.Remove(multiplex);
        }
    }

    private static void RemoveFilesFromInput(object parameter, InputVM input)
    {
        if (parameter is SourceFileInfo sourceFile)
        {
            if (!sourceFile.IsPrimary)
            {
                _ = input.SourceFiles.Remove(sourceFile);
                RemoveFilesFromTracks(input, sourceFile);
                RemoveFilesFromAttachments(input, sourceFile);
            }
        }
    }

    private static void RemoveFilesFromTracks(InputVM input, SourceFileInfo sourceFile)
    {
        List<TrackVM> tracks = input.Tracks.Where(t => t.SourceFile == sourceFile).ToList();
        foreach (TrackVM track in tracks)
        {
            _ = input.Tracks.Remove(track);
        }
    }

    private static void RemoveFilesFromAttachments(InputVM input, SourceFileInfo sourceFile)
    {
        List<Attachment> atts = input.Multiplex.Attachments.ExistingAttachments.Where(a => a.SourceFile == sourceFile).ToList();
        foreach (Attachment attachment in atts)
        {
            _ = input.Multiplex.Attachments.ExistingAttachments.Remove(attachment);
        }
    }

    private static void RemoveFilesFromAttachments(object parameter, AttachmentsVM attachments)
    {
        if (parameter is Attachment attachment)
        {
            _ = attachments.NewAttachments.Remove(attachment);
        }
    }
}
