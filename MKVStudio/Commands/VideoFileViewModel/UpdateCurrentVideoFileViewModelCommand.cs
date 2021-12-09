using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentVideoFileViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly VideoFileVM _video;
        private readonly FileOverviewVM _fileOverview;
        private readonly TracksVM _tracks;
        private readonly AudioEditVM _audioEdit;
        private readonly VideoEditVM _videoEdit;

        public UpdateCurrentVideoFileViewModelCommand(VideoFileVM videoFileViewModel,
                                                      FileOverviewVM fileOverviewViewModel,
                                                      TracksVM tracksViewModel,
                                                      AudioEditVM audioEditViewModel,
                                                      VideoEditVM videoEditViewModel)
        {
            _video = videoFileViewModel;
            _fileOverview = fileOverviewViewModel;
            _tracks = tracksViewModel;
            _audioEdit = audioEditViewModel;
            _videoEdit = videoEditViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes videoFileViewModelType)
            {
                switch (videoFileViewModelType)
                {
                    case ViewModelTypes.FileOverview:
                        _video.CurrentVideoFileViewModel = _fileOverview;
                        break;
                    case ViewModelTypes.Tracks:
                        _video.CurrentVideoFileViewModel = _tracks;
                        break;
                    case ViewModelTypes.AudioEdit:
                        _video.CurrentVideoFileViewModel = _audioEdit;
                        break;
                    case ViewModelTypes.VideoEdit:
                        _video.CurrentVideoFileViewModel = _videoEdit;
                        break;
                }
            }
        }
    }
}
