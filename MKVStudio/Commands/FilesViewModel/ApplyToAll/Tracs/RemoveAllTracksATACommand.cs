using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllTracksATACommand : ICommand
    {
        private readonly TracksATAVM _tracks;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllTracksATACommand(TracksATAVM tracks)
        {
            _tracks = tracks;
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
                        _tracks.VideoTracks.Clear();
                        break;
                    case ViewModelTypes.AudioTrack:
                        _tracks.AudioTracks.Clear();
                        break;
                    case ViewModelTypes.SubtitlesTrack:
                        _tracks.SubtitleTracks.Clear();
                        break;
                }
            }
        }
    }
}
