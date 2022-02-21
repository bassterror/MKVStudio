using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels;

public class AudioPropertiesVM : TrackProperties
{
    public IUtilitiesService Util { get; }
    public AudioProperties AudioProperties { get; set; }

    public AudioPropertiesVM(IUtilitiesService util, MKVMergeJ.Track track)
    {
        Util = util;
        AudioProperties = new(util, track);
    }
}
