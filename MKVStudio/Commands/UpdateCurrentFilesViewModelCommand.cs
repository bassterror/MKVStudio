using MKVStudio.Models;
using MKVStudio.State;
using MKVStudio.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentFilesViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IFilesNavigator _navigator;
        private readonly IRootViewModelFactory _viewModelFactory;
        private readonly Video _selectedVideo;

        public UpdateCurrentFilesViewModelCommand(IFilesNavigator navigator, IRootViewModelFactory viewModelFactory, Video selectedVideo)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
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
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewModelType);
            }
        }
    }
}
