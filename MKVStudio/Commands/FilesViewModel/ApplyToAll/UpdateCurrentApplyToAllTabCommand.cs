using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentApplyToAllTabCommand : ICommand
    {
        private readonly ApplyToAllVM _applyToAllVM;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public UpdateCurrentApplyToAllTabCommand(ApplyToAllVM applyToAllVM)
        {
            _applyToAllVM = applyToAllVM;
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
                        _applyToAllVM.CurrentApplyToAllTab = _applyToAllVM.Tracks;
                        break;
                    case ViewModelTypes.Attachments:
                        _applyToAllVM.CurrentApplyToAllTab = ApplyToAllVM.Attachments;
                        break;
                    case ViewModelTypes.FileOverview:
                        _applyToAllVM.CurrentApplyToAllTab = _applyToAllVM.FileOverview;
                        break;
                    case ViewModelTypes.AudioEdit:
                        _applyToAllVM.CurrentApplyToAllTab = ApplyToAllVM.AudioEdit;
                        break;
                    case ViewModelTypes.VideoEdit:
                        _applyToAllVM.CurrentApplyToAllTab = ApplyToAllVM.VideoEdit;
                        break;
                    case ViewModelTypes.Tags:
                        _applyToAllVM.CurrentApplyToAllTab = ApplyToAllVM.Tags;
                        break;
                }
            }
        }
    }
}
