using MKVStudio.Services;
using MKVStudio.State;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentVideoFileViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IVideoFileNavigator _navigator;
        private readonly string _arguments;
        private readonly string _processName;

        public UpdateCurrentVideoFileViewModelCommand(IVideoFileNavigator navigator, string arguments, string processName)
        {
            _navigator = navigator;
            _arguments = arguments;
            _processName = processName;
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
                        _navigator.CurrentVideoFileViewModel = new GeneralViewModel(FfmpegViewModel.LoadFfmpegViewModel(new FfmpegService(), _arguments, _processName));
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
