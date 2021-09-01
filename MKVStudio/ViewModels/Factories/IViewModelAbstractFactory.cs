using MKVStudio.State;

namespace MKVStudio.ViewModels.Factories
{
    public interface IViewModelAbstractFactory
    {
        BaseViewModel CreateViewModel(MainViewModelType mainViewModelType);
    }
}
