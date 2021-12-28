using MKVStudio.Models;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class OutputVM : BaseViewModel
{
    public MultiplexVM Multiplex { get; set; }
    public IExternalLibrariesService ExLib { get; }
    public string Title { get; set; }
    public ICommand RunMKVInfo { get; set; }
    public ICommand RunMKVExtract { get; set; }

    public OutputVM(MultiplexVM multiplex, MKVMergeJ result, IExternalLibrariesService exLib)
    {
        Multiplex = multiplex;
        ExLib = exLib;
        Title = result.Container.Properties.Title;
    }
}
