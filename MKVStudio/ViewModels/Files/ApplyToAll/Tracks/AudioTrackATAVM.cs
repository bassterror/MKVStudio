using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AudioTrackATAVM : BaseViewModel
    {
        private readonly ObservableCollection<AudioTrackATAVM> _audioTracks;

        public string Name { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagVisualImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public Language Language { get; set; }
        public IExternalLibrariesService ExLib { get; }
        public ICommand RemoveTrack => new RemoveTrackATACommand(null, _audioTracks, null, null, this, null);

        public AudioTrackATAVM(IExternalLibrariesService externalLibrariesService, ObservableCollection<AudioTrackATAVM> audioTracks)
        {
            ExLib = externalLibrariesService;
            _audioTracks = audioTracks;
        }
    }
}
