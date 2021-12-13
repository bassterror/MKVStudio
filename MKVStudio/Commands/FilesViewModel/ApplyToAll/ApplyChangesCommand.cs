using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class ApplyChangesCommand : ICommand
    {
        private readonly ObservableCollection<VideoFileVM> _videos;
        private readonly ApplyToAllVM _applyToAll;
        private readonly ApplyToAllV _applyToAllView;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public ApplyChangesCommand(ObservableCollection<VideoFileVM> videoFileViewModels, ApplyToAllVM applyToAllViewModel, ApplyToAllV applyToAllView)
        {
            _videos = videoFileViewModels;
            _applyToAll = applyToAllViewModel;
            _applyToAllView = applyToAllView;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //foreach (VideoFileVM video in _videos)
            //{
            //    video.Tracks.Title = _applyToAll.Title;

            //    if (video.Tracks.VideoTracks.Count != _applyToAll.VideoTracks.Count)
            //    {
            //        for (int i = 0; i < _applyToAll.VideoTracks.Count - video.Tracks.VideoTracks.Count; i++)
            //        {
            //            video.Tracks.VideoTracks.Add(new VideoTrackVM());
            //        }
            //    }
            //    if (video.Tracks.AudioTracks.Count != _applyToAll.AudioTracks.Count)
            //    {
            //        for (int i = 0; i < _applyToAll.AudioTracks.Count - video.Tracks.AudioTracks.Count; i++)
            //        {
            //            video.Tracks.AudioTracks.Add(new AudioTrackVM());
            //        }
            //    }
            //    if (video.Tracks.SubtitlesTracks.Count != _applyToAll.SubtitleTracks.Count)
            //    {
            //        for (int i = 0; i < _applyToAll.SubtitleTracks.Count - video.Tracks.SubtitlesTracks.Count; i++)
            //        {
            //            video.Tracks.SubtitlesTracks.Add(new SubtitlesTrackVM());
            //        }
            //    }
            //    if (_applyToAll.VideoTracks.Count > 0)
            //    {
            //        for (int i = 0; i < video.Tracks.VideoTracks.Count; i++)
            //        {
            //            video.Tracks.VideoTracks[i].Name = _applyToAll.VideoTracks[i].Name;
            //            video.Tracks.VideoTracks[i].Language = _applyToAll.VideoTracks[i].Language;
            //            video.Tracks.VideoTracks[i].DefaultTrack = _applyToAll.VideoTracks[i].DefaultTrack;
            //            video.Tracks.VideoTracks[i].EnabledTrack = _applyToAll.VideoTracks[i].EnabledTrack;
            //            video.Tracks.VideoTracks[i].ForcedTrack = _applyToAll.VideoTracks[i].ForcedTrack;
            //            video.Tracks.VideoTracks[i].FlagCommentary = _applyToAll.VideoTracks[i].FlagCommentary;
            //            video.Tracks.VideoTracks[i].FlagHearingImpaired = _applyToAll.VideoTracks[i].FlagHearingImpaired;
            //        }
            //    }
            //    if (_applyToAll.AudioTracks.Count > 0)
            //    {
            //        for (int i = 0; i < video.Tracks.AudioTracks.Count; i++)
            //        {
            //            video.Tracks.AudioTracks[i].Name = _applyToAll.AudioTracks[i].Name;
            //            video.Tracks.AudioTracks[i].Language = _applyToAll.AudioTracks[i].Language;
            //            video.Tracks.AudioTracks[i].DefaultTrack = _applyToAll.AudioTracks[i].DefaultTrack;
            //            video.Tracks.AudioTracks[i].EnabledTrack = _applyToAll.AudioTracks[i].EnabledTrack;
            //            video.Tracks.AudioTracks[i].ForcedTrack = _applyToAll.AudioTracks[i].ForcedTrack;
            //            video.Tracks.AudioTracks[i].FlagCommentary = _applyToAll.AudioTracks[i].FlagCommentary;
            //            video.Tracks.AudioTracks[i].FlagVisualImpaired = _applyToAll.AudioTracks[i].FlagVisualImpaired;
            //        }
            //    }
            //    if (_applyToAll.SubtitleTracks.Count > 0)
            //    {
            //        for (int i = 0; i < video.Tracks.SubtitlesTracks.Count; i++)
            //        {
            //            video.Tracks.SubtitlesTracks[i].Name = _applyToAll.SubtitleTracks[i].Name;
            //            video.Tracks.SubtitlesTracks[i].Language = _applyToAll.SubtitleTracks[i].Language;
            //            video.Tracks.SubtitlesTracks[i].DefaultTrack = _applyToAll.SubtitleTracks[i].DefaultTrack;
            //            video.Tracks.SubtitlesTracks[i].EnabledTrack = _applyToAll.SubtitleTracks[i].EnabledTrack;
            //            video.Tracks.SubtitlesTracks[i].ForcedTrack = _applyToAll.SubtitleTracks[i].ForcedTrack;
            //            video.Tracks.SubtitlesTracks[i].FlagCommentary = _applyToAll.SubtitleTracks[i].FlagCommentary;
            //            video.Tracks.SubtitlesTracks[i].FlagHearingImpaired = _applyToAll.SubtitleTracks[i].FlagHearingImpaired;
            //        }
            //    }
            //}
            _applyToAllView.Close();
        }
    }
}
