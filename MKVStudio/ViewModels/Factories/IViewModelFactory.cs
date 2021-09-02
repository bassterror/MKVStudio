using MKVStudio.Models;

namespace MKVStudio.ViewModels.Factories
{
    public interface IViewModelFactory<T> where T : BaseViewModel
    {
        T CreateViewModel();
    }
}
