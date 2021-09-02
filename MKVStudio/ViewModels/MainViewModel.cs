using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IMainNavigator Navigator { get; set; }

        public MainViewModel(IMainNavigator mainNavigator)
        {
            Navigator = mainNavigator;
            Navigator.UpdateCurrentMainViewModelCommand.Execute(ViewModelTypes.Files);
        }
    }
}
