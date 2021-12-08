using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class ApplyToAllViewModel
    {
        public IExternalLibrariesService ExLib { get; set; }
        public ApplyToAllView ApplyToAllView { get; }
        public ObservableCollection<VideoFileViewModel> Videos { get; set; }
        public string Title { get; set; }
        public ObservableCollection<VideoTrackAllViewModel> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrackAllViewModel> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitleTrackAllViewModel> SubtitleTracks { get; set; } = new();
        public ObservableCollection<AttachmentAllTrackViewModel> Attachments { get; set; } = new();
        public ICommand AddTrackAllCommand => new AddTrackAllCommand(ExLib, VideoTracks, AudioTracks, SubtitleTracks, Attachments);
        public ICommand RemoveTrackAllCommand => new RemoveTrackAllCommand(VideoTracks, AudioTracks, SubtitleTracks, Attachments);
        public ICommand ApplyChangesCommand => new ApplyChangesCommand(Videos, this, ApplyToAllView);

        public ApplyToAllViewModel(ObservableCollection<VideoFileViewModel> videos, IExternalLibrariesService externalLibrariesService, ApplyToAllView applyToAllView)
        {
            Videos = videos;
            ExLib = externalLibrariesService;
            ApplyToAllView = applyToAllView;
        }
    }
}
