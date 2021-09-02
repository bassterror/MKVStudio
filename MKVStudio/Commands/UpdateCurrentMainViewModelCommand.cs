﻿using MKVStudio.State;
using MKVStudio.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentMainViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IMainNavigator _navigator;
        private readonly IRootViewModelFactory _viewModelFactory;

        public UpdateCurrentMainViewModelCommand(IMainNavigator navigator, IRootViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
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
