using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TracksViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }
        public string Title { get; set; }
        public ObservableCollection<VideoTrackViewModel> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrackViewModel> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitleTrackViewModel> SubtitleTracks { get; set; } = new();
        public ObservableCollection<AttachmentTrackViewModel> Attachments { get; set; } = new();

        #region Commands
        public ICommand RunLoudnormFirstPassCommand { get; set; }
        public ICommand RunMKVInfoCommand { get; set; }
        public ICommand RunMKVExtractCommand { get; set; }
        #endregion

        public TracksViewModel(VideoFileViewModel selectedVideo, MKVMergeJ result, IExternalLibrariesService externalLibrariesService)
        {
            SelectedVideo = selectedVideo;
            RunLoudnormFirstPassCommand = new RunLoudnormFirstPassCommand(SelectedVideo, externalLibrariesService);
            RunMKVInfoCommand = new RunMKVInfoCommand(SelectedVideo, externalLibrariesService);
            RunMKVExtractCommand = new RunMKVExtractCommand(SelectedVideo, externalLibrariesService);

            Title = result.Container.Properties.Title;
            foreach (MKVMergeJ.Attachment attachment in result.Attachments)
            {
                Attachments.Add(new AttachmentTrackViewModel(SelectedVideo, attachment));
            }
            foreach (MKVMergeJ.Track videoTrack in result.Tracks.Where(v => v.Type == "video"))
            {
                VideoTracks.Add(new VideoTrackViewModel(SelectedVideo, videoTrack));
            }
            foreach (MKVMergeJ.Track audioTrack in result.Tracks.Where(v => v.Type == "audio"))
            {
                AudioTracks.Add(new AudioTrackViewModel(SelectedVideo, audioTrack));
            }
            foreach (MKVMergeJ.Track subtitleTrack in result.Tracks.Where(v => v.Type == "subtitles"))
            {
                SubtitleTracks.Add(new SubtitleTrackViewModel(SelectedVideo, subtitleTrack));
            }
        }
    }
}
