using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MKVStudio.Commands;

public class ClearFilesCommand : BaseCommand
{
    private readonly object _collectionParent;

    public ClearFilesCommand(object collectionParrent)
    {
        _collectionParent = collectionParrent;
    }

    public override void Execute(object parameter)
    {
        if (_collectionParent is MultiplexerVM multiplexer)
        {
            multiplexer.Multiplexes.Clear();
        }
        if (_collectionParent is InputVM input)
        {
            List<SourceFileInfo> notPrimary = input.SourceFiles.Where(s => !s.IsPrimary).ToList();
            foreach (SourceFileInfo sourceFile in notPrimary)
            {
                _ = input.SourceFiles.Remove(sourceFile);
                List<TrackVM> tracks = input.Tracks.Where(t => t.SourceFile == sourceFile).ToList();
                foreach (TrackVM track in tracks)
                {
                    _ = input.Tracks.Remove(track);
                }
                List<Attachment> att = input.Multiplex.Attachments.ExistingAttachments.Where(a => a.SourceFile == sourceFile).ToList();
                foreach (Attachment attachment in att)
                {
                    _ = input.Multiplex.Attachments.ExistingAttachments.Remove(attachment);
                }
            }
        }
        if (_collectionParent is AttachmentsVM attachments)
        {
            attachments.NewAttachments.Clear();
        }
    }
}
