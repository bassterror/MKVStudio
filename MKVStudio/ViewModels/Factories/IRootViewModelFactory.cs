using MKVStudio.State;

namespace MKVStudio.ViewModels.Factories
{
    public interface IRootViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewModelTypes viewModelType);
    }
}
