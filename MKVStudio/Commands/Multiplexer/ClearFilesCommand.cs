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
            List<SourceFileVM> notPrimary = input.SourceFiles.Where(s => !s.IsPrimary).ToList();
            foreach (SourceFileVM sourceFile in notPrimary)
            {
                _ = input.SourceFiles.Remove(sourceFile);
                List<TrackVM> tracks = input.Tracks.Where(t => t.SourceFile == sourceFile).ToList();
                foreach (TrackVM track in tracks)
                {
                    _ = input.Tracks.Remove(track);
                }
            }
        }
        if (_collectionParent is AttachmentsVM attachments)
        {
            attachments.NewAttachments.Clear();
        }
    }
}
