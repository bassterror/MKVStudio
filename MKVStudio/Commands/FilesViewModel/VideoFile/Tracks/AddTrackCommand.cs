using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddTrackCommand : ICommand
    {
        private readonly IExternalLibrariesService _exLib;
        private readonly TracksVM _tracks;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTrackCommand(IExternalLibrariesService exLib, TracksVM tracksVM)
        {
            _exLib = exLib;
            _tracks = tracksVM;
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
                        _tracks.VideoTracks.Add(new VideoTrackVM(_exLib, _tracks));
                        break;
                    case ViewModelTypes.AudioTrack:
                        _tracks.AudioTracks.Add(new AudioTrackVM(_exLib, _tracks));
                        break;
                    case ViewModelTypes.SubtitlesTrack:
                        _tracks.SubtitlesTracks.Add(new SubtitlesTrackVM(_exLib, _tracks));
                        break;
                }
            }
        }
    }
}
