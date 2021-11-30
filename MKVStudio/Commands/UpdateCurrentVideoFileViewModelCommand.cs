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
        private readonly INavigator _navigator;
        private readonly GeneralViewModel _generalViewModel;
        private readonly MediaInfoViewModel _mediaInfoViewModel;
        private readonly ConvertViewModel _convertViewModel;

        public UpdateCurrentVideoFileViewModelCommand(INavigator navigator, GeneralViewModel generalViewModel, MediaInfoViewModel mediaInfoViewModel, ConvertViewModel convertViewModel)
        {
            _navigator = navigator;
            _generalViewModel = generalViewModel;
            _mediaInfoViewModel = mediaInfoViewModel;
            _convertViewModel = convertViewModel;

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
                    case ViewModelTypes.General:
                        _navigator.CurrentVideoFileViewModel = _generalViewModel;
                        break;
                    case ViewModelTypes.MediaInfo:
                        _navigator.CurrentVideoFileViewModel = _mediaInfoViewModel;
                        break;
                    case ViewModelTypes.Convert:
                        _navigator.CurrentVideoFileViewModel = _convertViewModel;
                        break;
                    default:
                        throw new ArgumentException("No such VideoFileViewModelType");
                }
            }
        }
    }
}
