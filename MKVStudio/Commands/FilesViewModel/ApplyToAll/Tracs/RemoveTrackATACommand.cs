using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveTrackATACommand : ICommand
    {
        private readonly ObservableCollection<VideoTrackATAVM> _videoTracks;
        private readonly ObservableCollection<AudioTrackATAVM> _audioTracks;
        private readonly ObservableCollection<SubtitlesTrackATAVM> _subtitlesTracks;
        private readonly VideoTrackATAVM _videoTrack;
        private readonly AudioTrackATAVM _audioTrack;
        private readonly SubtitlesTrackATAVM _subtitlesTrack;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveTrackATACommand(ObservableCollection<VideoTrackATAVM> videoTracks = null,
            ObservableCollection<AudioTrackATAVM> audioTracks = null,
            ObservableCollection<SubtitlesTrackATAVM> subtitlesTracks = null,
            VideoTrackATAVM videoTrackAllVM = null,
            AudioTrackATAVM audioTrackAllVM = null,
            SubtitlesTrackATAVM subtitlesTrackAllVM = null)
        {
            _videoTracks = videoTracks;
            _audioTracks = audioTracks;
            _subtitlesTracks = subtitlesTracks;
            _videoTrack = videoTrackAllVM;
            _audioTrack = audioTrackAllVM;
            _subtitlesTrack = subtitlesTrackAllVM;
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
                        _videoTracks.Remove(_videoTrack);
                        break;
                    case ViewModelTypes.ATAAudioTrack:
                        _audioTracks.Remove(_audioTrack);
                        break;
                    case ViewModelTypes.ATASubtitleTrack:
                        _subtitlesTracks.Remove(_subtitlesTrack);
                        break;
                }
            }
        }
    }
}
