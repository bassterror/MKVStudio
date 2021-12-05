using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentVideoFileViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly VideoFileViewModel _video;
        private readonly FileOverviewViewModel _fileOverview;
        private readonly TracksViewModel _tracks;
        private readonly AudioEditViewModel _audioEdit;

        public UpdateCurrentVideoFileViewModelCommand(VideoFileViewModel videoFileViewModel,
                                                      FileOverviewViewModel fileOverviewViewModel,
                                                      TracksViewModel tracksViewModel,
                                                      AudioEditViewModel audioEditViewModel)
        {
            _video = videoFileViewModel;
            _fileOverview = fileOverviewViewModel;
            _tracks = tracksViewModel;
            _audioEdit = audioEditViewModel;
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
                    default:
                        throw new ArgumentException("No such VideoFileViewModelType");
                }
            }
        }
    }
}
