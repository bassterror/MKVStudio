using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class InputVM : BaseViewModel
{
    public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();
    public MultiplexVM Multiplex { get; }
    public IExternalLibrariesService ExLib { get; }

    #region SourceFiles
    public ObservableCollection<SourceFileVM> SourceFiles { get; set; } = new();
    public ICommand AddFiles => new AddFilesCommand(this, ExLib);
    public ICommand AddFilesFromFolder => new AddFilesFromFolderCommand(this, ExLib);
    public ICommand ClearFiles => new ClearFilesCommand(this);
    public ICommand RemoveFiles => new RemoveFilesCommand(this);
    #endregion

    #region Tracks
    public ObservableCollection<TrackVM> Tracks { get; set; } = new();
    public bool IsCheckAllT { get; set; }
    public bool IsUncheckAllT { get; set; }
    public TrackVM SelectedTrack { get; set; }
    public ICommand CheckAllTracks => new CheckBoxCommand(Tracks);
    #endregion

    public InputVM(MultiplexVM multiplex, IExternalLibrariesService exLib)
    {
        Multiplex = multiplex;
        ExLib = exLib;
        SourceFiles.Add(new SourceFileVM(multiplex.PrimarySourceFullPath, true, this));

        CreateTracks(SourceFiles.First());
    }

    public async void CreateTracks(SourceFileVM sourceFile)
    {
        ProcessResult pr = await ExLib.Run(ProcessResultNames.MKVMergeJ, sourceFile);
        ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
        MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);
        sourceFile.Type = result.Container.Type;
        foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "video"))
        {
            Tracks.Add(new TrackVM(this, sourceFile, track, TrackPropertiesTypes.Video, ExLib));
        }
        foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "audio"))
        {
            Tracks.Add(new TrackVM(this, sourceFile, track, TrackPropertiesTypes.Audio, ExLib));
        }
        foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "subtitles"))
        {
            Tracks.Add(new TrackVM(this, sourceFile, track, TrackPropertiesTypes.Subtitles, ExLib));
        }
        Multiplex.Output = new OutputVM(Multiplex, result, ExLib);
        Multiplex.Attachments = new AttachmentsVM(sourceFile, result.Attachments, ExLib);
        Multiplex.Chapters = new ChaptersVM();
    }
}
