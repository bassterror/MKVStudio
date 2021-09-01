using MKVStudio.State;
using System;

namespace MKVStudio.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        public BaseViewModel CreateViewModel(MainViewModelType mainViewModelType)
        {
            switch (mainViewModelType)
            {
                case MainViewModelType.Files:
                    return new FilesViewModel();
                case MainViewModelType.Queue:
                    return new QueueViewModel();
                default:
                    throw new ArgumentException("No such MainViewModelType");
            }
        }
    }
}
