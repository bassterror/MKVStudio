using MKVStudio.State.FilesNavigator;
using MKVStudio.ViewModels.Base;

namespace MKVStudio.ViewModels.Files
{
    public class FilesViewModel : BaseMainViewModel
    {
        public IFilesNavigator Navigator { get; set; } = new FilesNavigator();
    }
}
