using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels;

public class VideoPropertiesVM : TrackProperties
{
    public IUtilitiesService Util { get; }
    public VideoProperties VideoProperties { get; set; }

    public VideoPropertiesVM(IUtilitiesService util, MKVMergeJ.Track track)
    {
        Util = util;
        VideoProperties = new(Util, track);
    }

}
