using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllTracksATACommand : ICommand
    {
        private readonly ObservableCollection<VideoTrackATAVM> _videoTracks;
        private readonly ObservableCollection<AudioTrackATAVM> _audioTracks;
        private readonly ObservableCollection<SubtitlesTrackATAVM> _subtitleTracks;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllTracksATACommand(ObservableCollection<VideoTrackATAVM> videoTracks,
            ObservableCollection<AudioTrackATAVM> audioTracks,
            ObservableCollection<SubtitlesTrackATAVM> subtitleTracks)
        {
            _videoTracks = videoTracks;
            _audioTracks = audioTracks;
            _subtitleTracks = subtitleTracks;
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
                    case ViewModelTypes.ATAVideoTrack:
                        _videoTracks.Clear();
                        break;
                    case ViewModelTypes.ATAAudioTrack:
                        _audioTracks.Clear();
                        break;
                    case ViewModelTypes.ATASubtitleTrack:
                        _subtitleTracks.Clear();
                        break;
                }
            }
        }
    }
}
