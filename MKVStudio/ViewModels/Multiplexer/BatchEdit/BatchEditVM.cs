using MKVStudio.Services;
using MKVStudio.Views;

namespace MKVStudio.ViewModels;

public class BatchEditVM : BaseViewModel
{
    private readonly IUtilitiesService _util;

    public BatchEditV ApplyToAllView { get; }
    public BatchEditVM ThisApplyToAllVM { get; set; }
    public BaseViewModel CurrentApplyToAllTab { get; set; }
    public MultiplexerVM Multiplexer { get; set; }

    public BatchEditVM(MultiplexerVM multiplexer, IUtilitiesService util, BatchEditV applyToAllView)
    {
        _util = util;
        ApplyToAllView = applyToAllView;
        Multiplexer = multiplexer;
    }
}
