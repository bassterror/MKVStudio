using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class FilesViewModel : BaseViewModel
    {
        public IFilesNavigator Navigator { get; set; } = new FilesNavigator();
    }
}
