using MKVStudio.State.VideoFileNavigator;
using MKVStudio.ViewModels.VideoFile;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentVideoFileViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IVideoFileNavigator _navigator;

        public UpdateCurrentVideoFileViewModelCommand(IVideoFileNavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is VideoFileViewModelType videoFileViewModelType)
            {
                switch (videoFileViewModelType)
                {
                    case VideoFileViewModelType.General:
                        _navigator.CurrentVideoFileViewModel = new GeneralViewModel();
                        break;
                    case VideoFileViewModelType.MediaInfo:
                        _navigator.CurrentVideoFileViewModel = new MediaInfoViewModel();
                        break;
                    case VideoFileViewModelType.Convert:
                        _navigator.CurrentVideoFileViewModel = new ConvertViewModel();
                        break;
                    default:
                        throw new ArgumentException("No such VideoFileViewModelType");
                }
            }
        }
    }
}
