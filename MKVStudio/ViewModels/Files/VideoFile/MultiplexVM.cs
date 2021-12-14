using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class MultiplexVM : BaseViewModel
    {
        public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();
        public MultiplexerVM Multiplexer { get; }
        public IExternalLibrariesService ExLib { get; }
        public ICommand RemoveFile => new RemoveFileCommand(Multiplexer, this);
        public MultiplexVM ThisVideoFileVM { get; set; }
        public BaseViewModel CurrentVideoFileTab { get; set; }
        public ICommand UpdateCurrentVideoFileTab { get; set; }
        public TracksVM Tracks { get; set; }
        public AttachmentsVM Attachments { get; set; }
        public FileOverviewVM FileOverview { get; set; }
        public AudioEditVM AudioEdit { get; set; }
        public VideoEditVM VideoEdit { get; set; }
        public TagsVM Tags { get; set; }

        public MultiplexVM(MultiplexerVM multiplexer, string source, IExternalLibrariesService exLib)
        {
            ThisVideoFileVM = this;
            Multiplexer = multiplexer;
            ExLib = exLib;
            FileOverview = new FileOverviewVM(source, ExLib);
            AudioEdit = new AudioEditVM(this, ExLib);
            VideoEdit = new VideoEditVM(this, ExLib);

            CallMKVMergeJ();
        }

        private async void CallMKVMergeJ()
        {
            ProcessResult pr = await ExLib.Run(ProcessResultNames.MKVMergeJ, this);
            ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);

            Tracks = new TracksVM(this, result, ExLib);
            Attachments = new AttachmentsVM(result.Attachments);
            Tags = new TagsVM(result.Global_tags, result.Track_tags);

            UpdateCurrentVideoFileTab = new UpdateCurrentVideoFileTabCommand(this, FileOverview, Tracks, AudioEdit, VideoEdit, Tags, Attachments);
            UpdateCurrentVideoFileTab.Execute(ViewModelTypes.Tracks);
        }
    }
}
