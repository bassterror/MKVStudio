using MKVStudio.Commands;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public class Navigator : BaseNavigator, INavigator
    {
        private FilesViewModel FilesViewModel { get; set; }
        private QueueViewModel QueueViewModel { get; set; }

        public BaseViewModel CurrentMainViewModel { get; set; }
        public ICommand UpdateCurrentMainViewModelCommand { get; set; }

        public Navigator(IExternalLibrariesService externalLibrariesService)
        {
            FilesViewModel = new FilesViewModel(externalLibrariesService);
            QueueViewModel = new QueueViewModel(this);
            UpdateCurrentMainViewModelCommand = new UpdateCurrentMainViewModelCommand(this, FilesViewModel, QueueViewModel);
        }
    }
}
