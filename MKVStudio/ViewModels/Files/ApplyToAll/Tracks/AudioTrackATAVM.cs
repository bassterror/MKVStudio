using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AudioTrackATAVM : BaseViewModel
    {
        private readonly TracksATAVM _tracks;

        public string Name { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagVisualImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public Language Language { get; set; }
        public IExternalLibrariesService ExLib { get; }
        public ICommand RemoveTrack => new RemoveTrackATACommand(_tracks, null, this, null);

        public AudioTrackATAVM(IExternalLibrariesService externalLibrariesService, TracksATAVM tracks)
        {
            ExLib = externalLibrariesService;
            _tracks = tracks;
        }
    }
}
