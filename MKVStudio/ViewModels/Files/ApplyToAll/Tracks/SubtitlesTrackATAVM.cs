using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class SubtitlesTrackATAVM : BaseViewModel
    {
        private readonly ObservableCollection<SubtitlesTrackATAVM> _subtitlesTracks;

        public string Name { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagHearingImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public Language Language { get; set; }
        public IExternalLibrariesService ExLib { get; }
        public ICommand RemoveTrack => new RemoveTrackATACommand(null, null, _subtitlesTracks, null, null, this);

        public SubtitlesTrackATAVM(IExternalLibrariesService externalLibrariesService, ObservableCollection<SubtitlesTrackATAVM> subtitlesTracks)
        {
            ExLib = externalLibrariesService;
            _subtitlesTracks = subtitlesTracks;
        }
    }
}
