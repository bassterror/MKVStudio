using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class VideoTrackATAVM : BaseViewModel
    {
        private readonly ObservableCollection<VideoTrackATAVM> _videoTracks;

        public string Name { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagHearingImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public Language Language { get; set; }
        public IExternalLibrariesService ExLib { get; }
        public ICommand RemoveTrack => new RemoveTrackATACommand(_videoTracks, null, null, this, null, null);

        public VideoTrackATAVM(IExternalLibrariesService externalLibrariesService, ObservableCollection<VideoTrackATAVM> videoTracks)
        {
            ExLib = externalLibrariesService;
            _videoTracks = videoTracks;
        }
    }
}
