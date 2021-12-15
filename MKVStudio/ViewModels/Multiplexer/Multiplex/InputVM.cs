using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MKVStudio.ViewModels
{
    public class InputVM : BaseViewModel
    {
        public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();
        public MultiplexVM Multiplex { get; }
        public IExternalLibrariesService ExLib { get; }
        public ObservableCollection<SourceFileVM> SourceFiles { get; set; } = new();
        public ObservableCollection<TrackVM> Tracks { get; set; } = new();
        public TrackVM SelectedTrack { get; set; }
        public bool AreAllSFChecked
        {
            get => SourceFiles.Count == SourceFiles.Count(m => m.IsChecked);
            set
            {
                foreach (SourceFileVM sourceFile in SourceFiles)
                {
                    sourceFile.IsChecked = value;
                }
            }
        }
        public bool AreAllTChecked
        {
            get => Tracks.Count == Tracks.Count(m => m.IsChecked);
            set
            {
                foreach (TrackVM track in Tracks)
                {
                    track.IsChecked = value;
                }
            }
        }

        public InputVM(MultiplexVM multiplex, IExternalLibrariesService exLib)
        {
            Multiplex = multiplex;
            ExLib = exLib;
            SourceFiles.Add(new SourceFileVM(multiplex.PrimarySourceFullPath));
            CreateTracks(SourceFiles.First());
        }

        private async void CreateTracks(SourceFileVM sourceFile)
        {
            ProcessResult pr = await ExLib.Run(ProcessResultNames.MKVMergeJ, sourceFile);
            ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);
            foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "video"))
            {
                Tracks.Add(new TrackVM(this, track, TrackPropertiesTypes.Video, ExLib));
            }
            foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "audio"))
            {
                Tracks.Add(new TrackVM(this, track, TrackPropertiesTypes.Audio, ExLib));
            }
            foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "subtitles"))
            {
                Tracks.Add(new TrackVM(this, track, TrackPropertiesTypes.Subtitles, ExLib));
            }
            Multiplex.Output = new OutputVM(Multiplex, result, ExLib);
            Multiplex.Attachments = new AttachmentsVM(result.Attachments);
            Multiplex.Chapters = new ChaptersVM(result.Global_tags, result.Track_tags);
        }
    }
}
