using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class ApplyToAllVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public ApplyToAllV ApplyToAllView { get; }
        public ApplyToAllVM ThisApplyToAllVM { get; set; }
        public BaseViewModel CurrentApplyToAllTab { get; set; }
        public ICommand UpdateCurrentApplyToAllTab => new UpdateCurrentApplyToAllTabCommand(this);
        public ICommand ApplyChanges => new ApplyChangesCommand(Videos, this, ApplyToAllView);
        public ObservableCollection<VideoFileVM> Videos { get; set; }

        public TracksATAVM Tracks => new(_exLib);
        public static AttachmentsATAVM Attachments => new();
        public FileOverviewATAVM FileOverview => new(_exLib);
        public static AudioEditATAVM AudioEdit => new();
        public static VideoEditATAVM VideoEdit => new();
        public static TagsATAVM Tags => new();

        public ApplyToAllVM(ObservableCollection<VideoFileVM> videos, IExternalLibrariesService exLib, ApplyToAllV applyToAllView)
        {
            _exLib = exLib;
            ApplyToAllView = applyToAllView;
            ThisApplyToAllVM = this;
            Videos = videos;

            UpdateCurrentApplyToAllTab.Execute(ViewModelTypes.Tracks);
        }
    }
}
