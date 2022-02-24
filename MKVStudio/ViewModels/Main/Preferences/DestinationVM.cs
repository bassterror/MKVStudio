using MKVStudio.Services;

namespace MKVStudio.ViewModels;

public class DestinationVM : BaseViewModel
{
    public IUtilitiesService Util { get; }

    public DestinationVM(IUtilitiesService util)
    {
        Util = util;
    }
}
