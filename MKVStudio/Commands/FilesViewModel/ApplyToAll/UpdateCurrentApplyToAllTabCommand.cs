using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateCurrentApplyToAllTabCommand : ICommand
    {
        private readonly ApplyToAllVM _applyToAllVM;
        private readonly TracksATAVM _tracksAllVM;
        private readonly AttachmentsATAVM _attachmentsAllVM;
        private readonly FileOverviewATAVM _fileOverviewAllVM;
        private readonly AudioEditATAVM _audioEditAllVM;
        private readonly VideoEditATAVM _videoEditAllVM;
        private readonly TagsATAVM _tagsAllVM;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public UpdateCurrentApplyToAllTabCommand(ApplyToAllVM applyToAllVM,
            TracksATAVM tracksAllVM,
            AttachmentsATAVM attachmentsAllVM,
            FileOverviewATAVM fileOverviewAllVM,
            AudioEditATAVM audioEditAllVM,
            VideoEditATAVM videoEditAllVM,
            TagsATAVM tagsAllVM)
        {
            _applyToAllVM = applyToAllVM;
            _tracksAllVM = tracksAllVM;
            _attachmentsAllVM = attachmentsAllVM;
            _fileOverviewAllVM = fileOverviewAllVM;
            _audioEditAllVM = audioEditAllVM;
            _videoEditAllVM = videoEditAllVM;
            _tagsAllVM = tagsAllVM;
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
                    case ViewModelTypes.ATATracks:
                        _applyToAllVM.CurrentApplyToAllTab = _tracksAllVM;
                        break;
                    case ViewModelTypes.ATAAttachments:
                        _applyToAllVM.CurrentApplyToAllTab = _attachmentsAllVM;
                        break;
                    case ViewModelTypes.ATAFileOverview:
                        _applyToAllVM.CurrentApplyToAllTab = _fileOverviewAllVM;
                        break;
                    case ViewModelTypes.ATAAudioEdit:
                        _applyToAllVM.CurrentApplyToAllTab = _audioEditAllVM;
                        break;
                    case ViewModelTypes.ATAVideoEdit:
                        _applyToAllVM.CurrentApplyToAllTab = _videoEditAllVM;
                        break;
                    case ViewModelTypes.ATATags:
                        _applyToAllVM.CurrentApplyToAllTab = _tagsAllVM;
                        break;
                }
            }
        }
    }
}
