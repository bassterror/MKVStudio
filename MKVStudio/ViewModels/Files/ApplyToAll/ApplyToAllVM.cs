using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class ApplyToAllVM : BaseViewModel
    {
        public IExternalLibrariesService ExLib { get; set; }
        public ApplyToAllV ApplyToAllView { get; }
        public ObservableCollection<VideoFileVM> Videos { get; set; }
        public string Title { get; set; }
        public ObservableCollection<VideoTrackAllVM> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrackAllVM> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitlesTrackAllVM> SubtitleTracks { get; set; } = new();
        public ObservableCollection<AttachmentAllVM> Attachments { get; set; } = new();
        public ICommand AddTrackAll => new AddTrackAllCommand(ExLib, VideoTracks, AudioTracks, SubtitleTracks, Attachments);
        public ICommand RemoveTrackAll => new RemoveTrackAllCommand(VideoTracks, AudioTracks, SubtitleTracks, Attachments);
        public ICommand ApplyChanges => new ApplyChangesCommand(Videos, this, ApplyToAllView);

        public ApplyToAllVM(ObservableCollection<VideoFileVM> videos, IExternalLibrariesService exLib, ApplyToAllV applyToAllView)
        {
            Videos = videos;
            ExLib = exLib;
            ApplyToAllView = applyToAllView;
        }
    }
}
