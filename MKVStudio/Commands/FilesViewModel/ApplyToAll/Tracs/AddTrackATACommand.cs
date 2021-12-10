using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddTrackATACommand : ICommand
    {
        private readonly IExternalLibrariesService _exLib;
        private readonly ObservableCollection<VideoTrackATAVM> _videoTracks;
        private readonly ObservableCollection<AudioTrackATAVM> _audioTracks;
        private readonly ObservableCollection<SubtitlesTrackATAVM> _subtitleTracks;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTrackATACommand(IExternalLibrariesService exLib,
            ObservableCollection<VideoTrackATAVM> videoTracks,
            ObservableCollection<AudioTrackATAVM> audioTracks,
            ObservableCollection<SubtitlesTrackATAVM> subtitleTracks)
        {
            _exLib = exLib;
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
                        _videoTracks.Add(new VideoTrackATAVM(_exLib, _videoTracks));
                        break;
                    case ViewModelTypes.ATAAudioTrack:
                        _audioTracks.Add(new AudioTrackATAVM(_exLib, _audioTracks));
                        break;
                    case ViewModelTypes.ATASubtitleTrack:
                        _subtitleTracks.Add(new SubtitlesTrackATAVM(_exLib, _subtitleTracks));
                        break;
                }
            }
        }
    }
}
