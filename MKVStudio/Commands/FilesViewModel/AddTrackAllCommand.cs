using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddTrackAllCommand : ICommand
    {
        private readonly IExternalLibrariesService _exLib;
        private readonly ObservableCollection<VideoTrackAllVM> _videoTracks;
        private readonly ObservableCollection<AudioTrackAllVM> _audioTracks;
        private readonly ObservableCollection<SubtitlesTrackAllVM> _subtitleTracks;
        private readonly ObservableCollection<AttachmentAllVM> _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTrackAllCommand(IExternalLibrariesService externalLibrariesService,
            ObservableCollection<VideoTrackAllVM> videoTracks,
            ObservableCollection<AudioTrackAllVM> audioTracks,
            ObservableCollection<SubtitlesTrackAllVM> subtitleTracks,
            ObservableCollection<AttachmentAllVM> attachments)
        {
            _exLib = externalLibrariesService;
            _videoTracks = videoTracks;
            _audioTracks = audioTracks;
            _subtitleTracks = subtitleTracks;
            _attachments = attachments;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModelType)
            {
                switch (viewModelType)
{
                    case ViewModelTypes.VideoTrackAll:
                        _videoTracks.Add(new VideoTrackAllVM(_exLib));
                        break;
                    case ViewModelTypes.AudioTrackAll:
                        _audioTracks.Add(new AudioTrackAllVM(_exLib));
                        break;
                    case ViewModelTypes.SubtitleTrackAll:
                        _subtitleTracks.Add(new SubtitlesTrackAllVM(_exLib));
                        break;
                    case ViewModelTypes.AttachmentsAll:
                        _attachments.Add(new AttachmentAllVM());
                        break;
                }
            }
        }
    }
}
