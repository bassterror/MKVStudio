using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;

namespace MKVStudio.ViewModels;

public class SubtitlesPropertiesVM : TrackProperties
{
    public IUtilitiesService Util { get; }
    public SubtitlesProperties SubtitlesProperties { get; set; }

    public SubtitlesPropertiesVM(IUtilitiesService util, MKVMergeJ.Track track)
    {
        Util = util;
        SubtitlesProperties = new(Util, track);
    }
}
