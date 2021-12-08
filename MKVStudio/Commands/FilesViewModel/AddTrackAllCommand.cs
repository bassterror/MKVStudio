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
        private readonly ObservableCollection<VideoTrackAllViewModel> _videoTracks;
        private readonly ObservableCollection<AudioTrackAllViewModel> _audioTracks;
        private readonly ObservableCollection<SubtitleTrackAllViewModel> _subtitleTracks;
        private readonly ObservableCollection<AttachmentAllTrackViewModel> _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTrackAllCommand(IExternalLibrariesService externalLibrariesService,
            ObservableCollection<VideoTrackAllViewModel> videoTracks,
            ObservableCollection<AudioTrackAllViewModel> audioTracks,
            ObservableCollection<SubtitleTrackAllViewModel> subtitleTracks,
            ObservableCollection<AttachmentAllTrackViewModel> attachments)
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
                        _videoTracks.Add(new VideoTrackAllViewModel(_exLib));
                        break;
                    case ViewModelTypes.AudioTrackAll:
                        _audioTracks.Add(new AudioTrackAllViewModel(_exLib));
                        break;
                    case ViewModelTypes.SubtitleTrackAll:
                        _subtitleTracks.Add(new SubtitleTrackAllViewModel(_exLib));
                        break;
                    case ViewModelTypes.AttachmentsAll:
                        _attachments.Add(new AttachmentAllTrackViewModel());
                        break;
                }
            }
        }
    }
}
