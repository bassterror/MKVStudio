using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class QueueViewModel : BaseViewModel
    {
        public INavigator Navigator { get; }
        public QueueViewModel(INavigator mainNavigator)
        {
            Navigator = mainNavigator;
        }
    }
}
