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
            foreach (VideoFileVM video in _videos)
            {
                video.TracksViewModel.Title = _applyToAll.Title;

                if (video.TracksViewModel.VideoTracks.Count != _applyToAll.VideoTracks.Count)
                {
                    for (int i = 0; i < _applyToAll.VideoTracks.Count - video.TracksViewModel.VideoTracks.Count; i++)
                    {
                        video.TracksViewModel.VideoTracks.Add(new VideoTrackVM(video));
                    }
                }
                if (video.TracksViewModel.AudioTracks.Count != _applyToAll.AudioTracks.Count)
                {
                    for (int i = 0; i < _applyToAll.AudioTracks.Count - video.TracksViewModel.AudioTracks.Count; i++)
                    {
                        video.TracksViewModel.AudioTracks.Add(new AudioTrackVM(video));
                    }
                }
                if (video.TracksViewModel.SubtitlesTracks.Count != _applyToAll.SubtitleTracks.Count)
                {
                    for (int i = 0; i < _applyToAll.SubtitleTracks.Count - video.TracksViewModel.SubtitlesTracks.Count; i++)
                    {
                        video.TracksViewModel.SubtitlesTracks.Add(new SubtitlesTrackVM(video));
                    }
                }
                if (video.TracksViewModel.Attachments.Count != _applyToAll.Attachments.Count)
                {
                    for (int i = 0; i < _applyToAll.Attachments.Count - video.TracksViewModel.Attachments.Count; i++)
                    {
                        video.TracksViewModel.Attachments.Add(new AttachmentVM(video));
                    }
                }
                if (_applyToAll.VideoTracks.Count > 0)
                {
                    for (int i = 0; i < video.TracksViewModel.VideoTracks.Count; i++)
                    {
                        video.TracksViewModel.VideoTracks[i].Name = _applyToAll.VideoTracks[i].Name;
                        video.TracksViewModel.VideoTracks[i].Language = _applyToAll.VideoTracks[i].Language;
                        video.TracksViewModel.VideoTracks[i].DefaultTrack = _applyToAll.VideoTracks[i].DefaultTrack;
                        video.TracksViewModel.VideoTracks[i].EnabledTrack = _applyToAll.VideoTracks[i].EnabledTrack;
                        video.TracksViewModel.VideoTracks[i].ForcedTrack = _applyToAll.VideoTracks[i].ForcedTrack;
                        video.TracksViewModel.VideoTracks[i].FlagCommentary = _applyToAll.VideoTracks[i].FlagCommentary;
                        video.TracksViewModel.VideoTracks[i].FlagHearingImpaired = _applyToAll.VideoTracks[i].FlagHearingImpaired;
                    }
                }
                if (_applyToAll.AudioTracks.Count > 0)
                {
                    for (int i = 0; i < video.TracksViewModel.AudioTracks.Count; i++)
                    {
                        video.TracksViewModel.AudioTracks[i].Name = _applyToAll.AudioTracks[i].Name;
                        video.TracksViewModel.AudioTracks[i].Language = _applyToAll.AudioTracks[i].Language;
                        video.TracksViewModel.AudioTracks[i].DefaultTrack = _applyToAll.AudioTracks[i].DefaultTrack;
                        video.TracksViewModel.AudioTracks[i].EnabledTrack = _applyToAll.AudioTracks[i].EnabledTrack;
                        video.TracksViewModel.AudioTracks[i].ForcedTrack = _applyToAll.AudioTracks[i].ForcedTrack;
                        video.TracksViewModel.AudioTracks[i].FlagCommentary = _applyToAll.AudioTracks[i].FlagCommentary;
                        video.TracksViewModel.AudioTracks[i].FlagVisualImpaired = _applyToAll.AudioTracks[i].FlagVisualImpaired;
                    }
                }
                if (_applyToAll.SubtitleTracks.Count > 0)
                {
                    for (int i = 0; i < video.TracksViewModel.SubtitlesTracks.Count; i++)
                    {
                        video.TracksViewModel.SubtitlesTracks[i].Name = _applyToAll.SubtitleTracks[i].Name;
                        video.TracksViewModel.SubtitlesTracks[i].Language = _applyToAll.SubtitleTracks[i].Language;
                        video.TracksViewModel.SubtitlesTracks[i].DefaultTrack = _applyToAll.SubtitleTracks[i].DefaultTrack;
                        video.TracksViewModel.SubtitlesTracks[i].EnabledTrack = _applyToAll.SubtitleTracks[i].EnabledTrack;
                        video.TracksViewModel.SubtitlesTracks[i].ForcedTrack = _applyToAll.SubtitleTracks[i].ForcedTrack;
                        video.TracksViewModel.SubtitlesTracks[i].FlagCommentary = _applyToAll.SubtitleTracks[i].FlagCommentary;
                        video.TracksViewModel.SubtitlesTracks[i].FlagHearingImpaired = _applyToAll.SubtitleTracks[i].FlagHearingImpaired;
                    }
                }
                if (_applyToAll.Attachments.Count > 0)
                {
                    for (int i = 0; i < video.TracksViewModel.Attachments.Count; i++)
                    {
                        video.TracksViewModel.Attachments[i].Name = _applyToAll.Attachments[i].Name;
                        video.TracksViewModel.Attachments[i].ContentType = _applyToAll.Attachments[i].ContentType;
                        video.TracksViewModel.Attachments[i].Description = _applyToAll.Attachments[i].Description;
                    }
                }
            }
            _applyToAllView.Close();
        }
    }
}
