using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public MainVM ThisMainViewModel { get; set; }
        public BaseViewModel CurrentMainViewModel { get; set; }
        private FilesVM FilesViewModel { get; set; }
        private QueueVM QueueViewModel { get; set; }

        public ICommand UpdateCurrentMainViewModelCommand { get; set; }

        public MainVM(IExternalLibrariesService externalLibrariesService)
        {
            ThisMainViewModel = this;
            FilesViewModel = new FilesVM(externalLibrariesService);
            QueueViewModel = new QueueVM();
            UpdateCurrentMainViewModelCommand = new UpdateCurrentMainViewModelCommand(this, FilesViewModel, QueueViewModel);
            UpdateCurrentMainViewModelCommand.Execute(ViewModelTypes.Files);
        }
    }
}
