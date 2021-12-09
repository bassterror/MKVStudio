using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TracksVM : BaseViewModel
    {
        public VideoFileVM SelectedVideo { get; set; }
        public string Title { get; set; }
        public ObservableCollection<VideoTrackVM> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrackVM> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitlesTrackVM> SubtitlesTracks { get; set; } = new();
        public ObservableCollection<AttachmentVM> Attachments { get; set; } = new();
        public ObservableCollection<GlobalTagVM> GlobalTags { get; set; } = new();
        public ObservableCollection<TrackTagVM> TrackTags { get; set; } = new();

        #region Commands
        public ICommand RunMKVInfo { get; set; }
        public ICommand RunMKVExtract { get; set; }
        #endregion

        public TracksVM(VideoFileVM selectedVideo, MKVMergeJ result, IExternalLibrariesService exLib)
        {
            SelectedVideo = selectedVideo;
            RunMKVInfo = new RunMKVInfoCommand(SelectedVideo, exLib);
            RunMKVExtract = new RunMKVExtractCommand(SelectedVideo, exLib);

            Title = result.Container.Properties.Title;
            foreach (MKVMergeJ.Track videoTrack in result.Tracks.Where(v => v.Type == "video"))
            {
                VideoTracks.Add(new VideoTrackVM(SelectedVideo, videoTrack, exLib));
            }
            foreach (MKVMergeJ.Track audioTrack in result.Tracks.Where(v => v.Type == "audio"))
            {
                AudioTracks.Add(new AudioTrackVM(SelectedVideo, audioTrack, exLib));
            }
            foreach (MKVMergeJ.Track subtitlesTrack in result.Tracks.Where(v => v.Type == "subtitles"))
            {
                SubtitlesTracks.Add(new SubtitlesTrackVM(SelectedVideo, subtitlesTrack, exLib));
            }
            foreach (MKVMergeJ.Attachment attachment in result.Attachments)
            {
                Attachments.Add(new AttachmentVM(SelectedVideo, attachment));
            }
            foreach (MKVMergeJ.Global_Tag globalTag in result.Global_tags)
            {
                GlobalTags.Add(new GlobalTagVM(SelectedVideo, globalTag));
            }
            foreach (MKVMergeJ.Track_Tag trackTad in result.Track_tags)
            {
                TrackTags.Add(new TrackTagVM(SelectedVideo, trackTad));
            }
        }
    }
}
