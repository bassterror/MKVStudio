using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllTracksCommand : ICommand
    {
        private readonly TracksVM _tracks;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllTracksCommand(TracksVM tracks)
        {
            _tracks = tracks;
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
                        _tracks.VideoTracks.Clear();
                        break;
                    case ViewModelTypes.AudioTrack:
                        _tracks.AudioTracks.Clear();
                        break;
                    case ViewModelTypes.SubtitlesTrack:
                        _tracks.SubtitlesTracks.Clear();
                        break;
                }
            }
        }
    }
}
