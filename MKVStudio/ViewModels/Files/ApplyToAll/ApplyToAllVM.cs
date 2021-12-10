using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class ApplyToAllVM : BaseViewModel
    {
        public ApplyToAllV ApplyToAllView { get; }
        public ApplyToAllVM ThisApplyToAllVM { get; set; }
        public BaseViewModel CurrentApplyToAllTab { get; set; }
        public ICommand UpdateCurrentApplyToAllTab { get; set; }
        public ICommand ApplyChanges => new ApplyChangesCommand(Videos, this, ApplyToAllView);
        public ObservableCollection<VideoFileVM> Videos { get; set; }

        public TracksATAVM Tracks { get; set; }
        public AttachmentsATAVM Attachments { get; set; }
        public FileOverviewATAVM FileOverview { get; set; }
        public AudioEditATAVM AudioEdit { get; set; }
        public VideoEditATAVM VideoEdit { get; set; }
        public TagsATAVM Tags { get; set; }

        public ApplyToAllVM(ObservableCollection<VideoFileVM> videos, IExternalLibrariesService exLib, ApplyToAllV applyToAllView)
        {
            ApplyToAllView = applyToAllView;
            ThisApplyToAllVM = this;
            Videos = videos;

            Tracks = new TracksATAVM(exLib);
            Attachments = new AttachmentsATAVM();
            FileOverview = new FileOverviewATAVM(exLib);
            AudioEdit = new AudioEditATAVM();
            VideoEdit = new VideoEditATAVM();
            Tags = new TagsATAVM();

            UpdateCurrentApplyToAllTab = new UpdateCurrentApplyToAllTabCommand(this, Tracks, Attachments, FileOverview, AudioEdit, VideoEdit, Tags);
            UpdateCurrentApplyToAllTab.Execute(ViewModelTypes.ATATracks);
        }
    }
}
