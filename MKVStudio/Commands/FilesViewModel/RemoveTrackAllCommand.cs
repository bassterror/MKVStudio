using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveTrackAllCommand : ICommand
    {
        private readonly ObservableCollection<VideoTrackAllVM> _videoTracks;
        private readonly ObservableCollection<AudioTrackAllVM> _audioTracks;
        private readonly ObservableCollection<SubtitlesTrackAllVM> _subtitleTracks;
        private readonly ObservableCollection<AttachmentAllVM> _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveTrackAllCommand(ObservableCollection<VideoTrackAllVM> videoTracks,
            ObservableCollection<AudioTrackAllVM> audioTracks,
            ObservableCollection<SubtitlesTrackAllVM> subtitleTracks,
            ObservableCollection<AttachmentAllVM> attachments)
        {
            _videoTracks = videoTracks;
            _audioTracks = audioTracks;
            _subtitleTracks = subtitleTracks;
            _attachments = attachments;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModelType)
            {
                switch (viewModelType)
                {
                    case ViewModelTypes.VideoTrackAll:
                        _videoTracks.RemoveAt(_videoTracks.Count - 1);
                        break;
                    case ViewModelTypes.AudioTrackAll:
                        _audioTracks.RemoveAt(_audioTracks.Count - 1);
                        break;
                    case ViewModelTypes.SubtitleTrackAll:
                        _subtitleTracks.RemoveAt(_subtitleTracks.Count - 1);
                        break;
                    case ViewModelTypes.AttachmentsAll:
                        _attachments.RemoveAt(_attachments.Count - 1);
                        break;
                }
            }
        }
    }
}
