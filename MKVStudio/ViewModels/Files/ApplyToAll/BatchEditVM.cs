using MKVStudio.Commands;
using MKVStudio.Services;
using MKVStudio.Views;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class BatchEditVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public BatchEditV ApplyToAllView { get; }
        public BatchEditVM ThisApplyToAllVM { get; set; }
        public BaseViewModel CurrentApplyToAllTab { get; set; }
        public ICommand UpdateCurrentApplyToAllTab => new UpdateCurrentApplyToAllTabCommand(this);
        public ICommand ApplyChanges => new ApplyChangesCommand(Multiplexer, this, ApplyToAllView);
        public MultiplexerVM Multiplexer { get; set; }

        public TracksATAVM Tracks => new(_exLib);
        public static AttachmentsATAVM Attachments => new();
        public FileOverviewATAVM FileOverview => new(_exLib);
        public static AudioEditATAVM AudioEdit => new();
        public static VideoEditATAVM VideoEdit => new();
        public static TagsATAVM Tags => new();

        public BatchEditVM(MultiplexerVM multiplexer, IExternalLibrariesService exLib, BatchEditV applyToAllView)
        {
            _exLib = exLib;
            ApplyToAllView = applyToAllView;
            ThisApplyToAllVM = this;
            Multiplexer = multiplexer;

            UpdateCurrentApplyToAllTab.Execute(ViewModelTypes.Tracks);
        }
    }
}
