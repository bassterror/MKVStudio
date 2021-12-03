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
        private readonly FileOverviewViewModel _generalViewModel;
        private readonly TracksViewModel _mediaInfoViewModel;
        private readonly ConvertViewModel _convertViewModel;

        public UpdateCurrentVideoFileViewModelCommand(INavigator navigator, FileOverviewViewModel generalViewModel, TracksViewModel mediaInfoViewModel, ConvertViewModel convertViewModel)
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
                    case ViewModelTypes.FileOverview:
                        _navigator.CurrentVideoFileViewModel = _generalViewModel;
                        break;
                    case ViewModelTypes.Tracks:
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
