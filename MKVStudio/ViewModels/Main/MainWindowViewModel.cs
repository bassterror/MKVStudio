using MKVStudio.State.MainNavigator;
using MKVStudio.ViewModels.Base;

namespace MKVStudio.ViewModels.Main
{
    public class MainWindowViewModel : BaseMainViewModel
    {
        public IMainNavigator Navigator { get; set; } = new MainNavigator();
    }
}
