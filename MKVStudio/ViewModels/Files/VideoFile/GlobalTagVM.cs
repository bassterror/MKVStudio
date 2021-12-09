using MKVStudio.Models;

namespace MKVStudio.ViewModels
{
    public class GlobalTagVM : BaseViewModel
    {
        public VideoFileVM SelectedVideo { get; set; }
        public int NumEntries { get; set; }

        public GlobalTagVM(VideoFileVM videoFileVM, MKVMergeJ.Global_Tag globalTag = null)
        {
            if (globalTag != null)
            {
                SelectedVideo = videoFileVM;
                NumEntries = globalTag.Num_entries;
            }
        }
    }
}
