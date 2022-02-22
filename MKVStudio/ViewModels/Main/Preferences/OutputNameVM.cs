using MKVStudio.Services;

namespace MKVStudio.ViewModels;

public class OutputNameVM : BaseViewModel
{
    public IUtilitiesService Util { get; }

    public OutputNameVM(IUtilitiesService util)
    {
        Util = util;
    }
}
