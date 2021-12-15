using MKVStudio.Services;
using MKVStudio.Views;

namespace MKVStudio.ViewModels
{
    public class BatchEditVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public BatchEditV ApplyToAllView { get; }
        public BatchEditVM ThisApplyToAllVM { get; set; }
        public BaseViewModel CurrentApplyToAllTab { get; set; }
        public MultiplexerVM Multiplexer { get; set; }

        public BatchEditVM(MultiplexerVM multiplexer, IExternalLibrariesService exLib, BatchEditV applyToAllView)
        {
            _exLib = exLib;
            ApplyToAllView = applyToAllView;
            Multiplexer = multiplexer;
        }
    }
}
