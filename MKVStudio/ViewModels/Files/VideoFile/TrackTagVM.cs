using MKVStudio.Models;

namespace MKVStudio.ViewModels
{
    public class TrackTagVM : BaseViewModel
    {
        public VideoFileVM SelectedVideo { get; set; }
        public int NumEntries { get; set; }
        public int TrackID { get; set; }

        public TrackTagVM(VideoFileVM videoFileVM, MKVMergeJ.Track_Tag trackTag)
        {
            if (trackTag != null)
            {
                SelectedVideo = videoFileVM;
                NumEntries = trackTag.Num_entries;
                TrackID = trackTag.Track_id;
            }
        }
    }
}
