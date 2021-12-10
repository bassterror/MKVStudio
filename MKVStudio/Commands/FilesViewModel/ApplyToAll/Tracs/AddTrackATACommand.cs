using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddTrackATACommand : ICommand
    {
        private readonly IExternalLibrariesService _exLib;
        private readonly TracksATAVM _tracks;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTrackATACommand(IExternalLibrariesService exLib, TracksATAVM tracks)
        {
            _exLib = exLib;
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
                        _tracks.VideoTracks.Add(new VideoTrackATAVM(_exLib, _tracks));
                        break;
                    case ViewModelTypes.AudioTrack:
                        _tracks.AudioTracks.Add(new AudioTrackATAVM(_exLib, _tracks));
                        break;
                    case ViewModelTypes.SubtitlesTrack:
                        _tracks.SubtitleTracks.Add(new SubtitlesTrackATAVM(_exLib, _tracks));
                        break;
                }
            }
        }
    }
}
