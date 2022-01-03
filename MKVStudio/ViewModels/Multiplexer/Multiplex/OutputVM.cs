using MKVStudio.Models;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class OutputVM : BaseViewModel
{
    public MultiplexVM Multiplex { get; set; }
    public IUtilitiesService Util { get; }
    public string Title { get; set; }
    public ICommand RunMKVInfo { get; set; }
    public ICommand RunMKVExtract { get; set; }

    public OutputVM(MultiplexVM multiplex, MKVMergeJ result, IUtilitiesService util)
    {
        Multiplex = multiplex;
        Util = util;
        Title = result.Container.Properties.Title;
    }
}
