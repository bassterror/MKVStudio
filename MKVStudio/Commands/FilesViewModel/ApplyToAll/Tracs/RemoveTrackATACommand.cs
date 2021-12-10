using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveTrackATACommand : ICommand
    {
        private readonly TracksATAVM _tracks;
        private readonly VideoTrackATAVM _videoTrack;
        private readonly AudioTrackATAVM _audioTrack;
        private readonly SubtitlesTrackATAVM _subtitlesTrack;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveTrackATACommand(TracksATAVM tracks,
            VideoTrackATAVM videoTrackAllVM = null,
            AudioTrackATAVM audioTrackAllVM = null,
            SubtitlesTrackATAVM subtitlesTrackAllVM = null)
        {
            _tracks = tracks;
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
                    case ViewModelTypes.VideoTrack:
                        _tracks.VideoTracks.Remove(_videoTrack);
                        break;
                    case ViewModelTypes.AudioTrack:
                        _tracks.AudioTracks.Remove(_audioTrack);
                        break;
                    case ViewModelTypes.SubtitlesTrack:
                        _tracks.SubtitleTracks.Remove(_subtitlesTrack);
                        break;
                }
            }
        }
    }
}
