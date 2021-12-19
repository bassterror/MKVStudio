using MKVStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class ClearFilesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly object _collectionParent;

        public ClearFilesCommand(object collectionParrent)
        {
            _collectionParent = collectionParrent;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
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
}
