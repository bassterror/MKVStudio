namespace MKVStudio.ViewModels.Factories
{
    public class FilesViewModelFactory : IViewModelFactory<FilesViewModel>
    {
        public FilesViewModel CreateViewModel()
        {
            return new FilesViewModel();
        }
    }
}
