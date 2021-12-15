using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class OutputVM : BaseViewModel
    {
        public MultiplexVM SelectedVideo { get; set; }
        public string Title { get; set; }
        public ICommand RunMKVInfo { get; set; }
        public ICommand RunMKVExtract { get; set; }

        public OutputVM(MultiplexVM selectedVideo, MKVMergeJ result, IExternalLibrariesService exLib)
        {
            SelectedVideo = selectedVideo;

            Title = result.Container.Properties.Title;
        }
    }
}
