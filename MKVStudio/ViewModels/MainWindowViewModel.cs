using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public IMainNavigator Navigator { get; set; } = new MainNavigator();
        public MainWindowViewModel()
        {
            Navigator.UpdateCurrentMainViewModelCommand.Execute(MainViewModelType.Files);
        }
    }
}
