using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentVideoFileTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MultiplexVM _video;
        private readonly TracksVM _tracks;
        private readonly FileOverviewVM _fileOverview;
        private readonly AudioEditVM _audioEdit;
        private readonly VideoEditVM _videoEdit;
        private readonly TagsVM _tags;
        private readonly AttachmentsVM _attachments;

        public UpdateCurrentVideoFileTabCommand(MultiplexVM videoFileVM,
            FileOverviewVM fileOverviewVM,
            TracksVM tracksVM,
            AudioEditVM audioEditVM,
            VideoEditVM videoEditVM,
            TagsVM tagsVM,
            AttachmentsVM attachmentVM)
        {
            _video = videoFileVM;
            _fileOverview = fileOverviewVM;
            _tracks = tracksVM;
            _audioEdit = audioEditVM;
            _videoEdit = videoEditVM;
            _tags = tagsVM;
            _attachments = attachmentVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes videoFileTab)
            {
                switch (videoFileTab)
                {
                    case ViewModelTypes.Tracks:
                        _video.CurrentVideoFileTab = _tracks;
                        break;
                    case ViewModelTypes.FileOverview:
                        _video.CurrentVideoFileTab = _fileOverview;
                        break;
                    case ViewModelTypes.AudioEdit:
                        _video.CurrentVideoFileTab = _audioEdit;
                        break;
                    case ViewModelTypes.VideoEdit:
                        _video.CurrentVideoFileTab = _videoEdit;
                        break;
                    case ViewModelTypes.Tags:
                        _video.CurrentVideoFileTab = _tags;
                        break;
                    case ViewModelTypes.Attachments:
                        _video.CurrentVideoFileTab = _attachments;
                        break;
                }
            }
        }
    }
}
