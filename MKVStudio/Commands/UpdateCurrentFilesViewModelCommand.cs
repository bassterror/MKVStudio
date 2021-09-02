using MKVStudio.Models;
using MKVStudio.State;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentFilesViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly Video _selectedVideo;

        public UpdateCurrentFilesViewModelCommand(INavigator navigator, Video selectedVideo)
        {
            _navigator = navigator;
            _selectedVideo = selectedVideo;
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
                    case ViewModelTypes.VideoFile:
                        _navigator.CurrentFilesViewModel = new VideoFileViewModel(_navigator, _selectedVideo);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
