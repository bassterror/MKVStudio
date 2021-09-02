namespace MKVStudio.ViewModels.Factories
{
    public class QueueViewModelFactory : IViewModelFactory<QueueViewModel>
    {
        public QueueViewModel CreateViewModel()
        {
            return new QueueViewModel();
        }
    }
}
