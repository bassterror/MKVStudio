using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels;

public class OutputVM : BaseViewModel
{
    private readonly IUtilitiesService _util;
    private readonly MultiplexVM _multiplex;
    public Output Output { get; set; }

    public OutputVM(IUtilitiesService util, MultiplexVM multiplex)
    {
        _util = util;
        _multiplex = multiplex;
        Output = new(_util);
    }
}
