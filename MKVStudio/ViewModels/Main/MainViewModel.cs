using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }

        public MainViewModel(INavigator mainNavigator)
        {
            Navigator = mainNavigator;
            Navigator.UpdateCurrentMainViewModelCommand.Execute(ViewModelTypes.Files);
        }
    }
}
