using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class VideoFileVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();
        public VideoFileVM ThisVideoFileVM { get; set; }
        public BaseViewModel CurrentVideoFileTab { get; set; }
        public ICommand UpdateCurrentVideoFileTab { get; set; }
        public TracksVM Tracks { get; set; }
        public AttachmentsVM Attachments { get; set; }
        public FileOverviewVM FileOverview { get; set; }
        public AudioEditVM AudioEdit { get; set; }
        public VideoEditVM VideoEdit { get; set; }
        public TagsVM Tags { get; set; }

        public VideoFileVM(string source, IExternalLibrariesService exLibService)
        {
            ThisVideoFileVM = this;
            _exLib = exLibService;
            FileOverview = new FileOverviewVM(source, _exLib);
            AudioEdit = new AudioEditVM(this, _exLib);
            VideoEdit = new VideoEditVM(this, _exLib);

            CallMKVMergeJ();
        }

        private async void CallMKVMergeJ()
        {
            ProcessResult pr = await _exLib.Run(ProcessResultNames.MKVMergeJ, this);
            ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);

            Tracks = new TracksVM(this, result, _exLib);
            Attachments = new AttachmentsVM(result.Attachments);
            Tags = new TagsVM(result.Global_tags, result.Track_tags);

            UpdateCurrentVideoFileTab = new UpdateCurrentVideoFileTabCommand(this, FileOverview, Tracks, AudioEdit, VideoEdit, Tags, Attachments);
            UpdateCurrentVideoFileTab.Execute(ViewModelTypes.Tracks);
        }
    }
}
