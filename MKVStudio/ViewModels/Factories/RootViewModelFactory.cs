using MKVStudio.State;
using System;

namespace MKVStudio.ViewModels.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IViewModelFactory<FilesViewModel> _filesViewModelFactory;
        private readonly IViewModelFactory<QueueViewModel> _queueViewModelFactory;
        private readonly IViewModelFactory<VideoFileViewModel> _videoFileViewModelFactory;

        public RootViewModelFactory(IViewModelFactory<FilesViewModel> filesViewModelFactory, IViewModelFactory<QueueViewModel> queueViewModelFactory, IViewModelFactory<VideoFileViewModel> videoFileViewModelFactory)
        {
            _filesViewModelFactory = filesViewModelFactory;
            _queueViewModelFactory = queueViewModelFactory;
            _videoFileViewModelFactory = videoFileViewModelFactory;
        }

        public BaseViewModel CreateViewModel(ViewModelTypes viewModelType)
        {
            switch (viewModelType)
            {
                case ViewModelTypes.Files:
                    return _filesViewModelFactory.CreateViewModel();
                case ViewModelTypes.Queue:
                    return _queueViewModelFactory.CreateViewModel();
                case ViewModelTypes.VideoFile:
                    return _videoFileViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("No such ViewModelType");
            }
        }
    }
}
