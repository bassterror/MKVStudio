using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private FilesViewModel FilesViewModel { get; set; }
        private QueueViewModel QueueViewModel { get; set; }

        public MainViewModel ThisMainViewModel { get; set; }
        public BaseViewModel CurrentMainViewModel { get; set; }
        public ICommand UpdateCurrentMainViewModelCommand { get; set; }

        public MainViewModel(IExternalLibrariesService externalLibrariesService)
        {
            ThisMainViewModel = this;
            FilesViewModel = new FilesViewModel(externalLibrariesService);
            QueueViewModel = new QueueViewModel();
            UpdateCurrentMainViewModelCommand = new UpdateCurrentMainViewModelCommand(this, FilesViewModel, QueueViewModel);
            UpdateCurrentMainViewModelCommand.Execute(ViewModelTypes.Files);
        }
    }
}
