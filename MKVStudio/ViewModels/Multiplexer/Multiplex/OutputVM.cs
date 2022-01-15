using MKVStudio.Models;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class OutputVM : BaseViewModel
{
    public IUtilitiesService Util { get; }
    public MultiplexVM Multiplex { get; set; }
    public string Title { get; set; }
    public ICommand RunMKVInfo { get; set; }
    public ICommand RunMKVExtract { get; set; }

    public OutputVM(IUtilitiesService util, MultiplexVM multiplex)
    {
        Util = util;
        Multiplex = multiplex;
    }
}
