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
        public MultiplexVM SelectedVideo { get; set; }
        public string Title { get; set; }
        public ObservableCollection<VideoTrackVM> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrackVM> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitlesTrackVM> SubtitlesTracks { get; set; } = new();
        public ICommand AddTrack { get; set; }
        public ICommand RemoveAllTracks => new RemoveAllTracksCommand(this);
        public ICommand RunMKVInfo { get; set; }
        public ICommand RunMKVExtract { get; set; }

        public TracksVM(MultiplexVM selectedVideo, MKVMergeJ result, IExternalLibrariesService exLib)
        {
            SelectedVideo = selectedVideo;
            RunMKVInfo = new RunMKVInfoCommand(SelectedVideo, exLib);
            RunMKVExtract = new RunMKVExtractCommand(SelectedVideo, exLib);

            Title = result.Container.Properties.Title;
            foreach (MKVMergeJ.Track videoTrack in result.Tracks.Where(v => v.Type == "video"))
            {
                VideoTracks.Add(new VideoTrackVM(exLib, this, videoTrack));
            }
            foreach (MKVMergeJ.Track audioTrack in result.Tracks.Where(v => v.Type == "audio"))
            {
                AudioTracks.Add(new AudioTrackVM(exLib, this, audioTrack));
            }
            foreach (MKVMergeJ.Track subtitlesTrack in result.Tracks.Where(v => v.Type == "subtitles"))
            {
                SubtitlesTracks.Add(new SubtitlesTrackVM(exLib, this, subtitlesTrack));
            }

            AddTrack = new AddTrackCommand(exLib, this);
        }
    }
}
