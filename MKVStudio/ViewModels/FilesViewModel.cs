using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class FilesViewModel : BaseViewModel
    {
        public INavigator Navigator { get; }
        public FilesViewModel(INavigator mainNavigator)
        {
            Navigator = mainNavigator;
        }
    }
}
