using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveTrackCommand : ICommand
    {
        private readonly TracksVM _tracks;
        private readonly VideoTrackVM _videoTrack;
        private readonly AudioTrackVM _audioTrack;
        private readonly SubtitlesTrackVM _subtitlesTrack;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveTrackCommand(TracksVM tracks,
            VideoTrackVM videoTrack = null,
            AudioTrackVM audioTrack = null,
            SubtitlesTrackVM subtitlesTrack = null)
        {
            _tracks = tracks;
            _videoTrack = videoTrack;
            _audioTrack = audioTrack;
            _subtitlesTrack = subtitlesTrack;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModel)
            {
                switch (viewModel)
                {
                    case ViewModelTypes.VideoTrack:
                        _tracks.VideoTracks.Remove(_videoTrack);
                        break;
                    case ViewModelTypes.AudioTrack:
                        _tracks.AudioTracks.Remove(_audioTrack);
                        break;
                    case ViewModelTypes.SubtitlesTrack:
                        _tracks.SubtitlesTracks.Remove(_subtitlesTrack);
                        break;
                }
            }
        }
    }
}
