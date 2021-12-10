using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TracksATAVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public string Title { get; set; }
        public ObservableCollection<VideoTrackATAVM> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrackATAVM> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitlesTrackATAVM> SubtitleTracks { get; set; } = new();
        public ICommand AddTrack => new AddTrackATACommand(_exLib, VideoTracks, AudioTracks, SubtitleTracks);
        public ICommand RemoveAllTracks => new RemoveAllTracksATACommand(VideoTracks, AudioTracks, SubtitleTracks);

        public TracksATAVM(IExternalLibrariesService exLib)
        {
            _exLib = exLib;
        }
    }
}
