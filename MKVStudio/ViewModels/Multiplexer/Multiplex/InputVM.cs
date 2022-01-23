﻿using MKVStudio.Commands;
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
    public IUtilitiesService Util { get; }

    #region SourceFiles
    public ObservableCollection<SourceFileInfo> SourceFiles { get; set; } = new();
    public ICommand AddFiles => new AddFilesCommand(this, Util);
    public ICommand AddFilesFromFolder => new AddFilesFromFolderCommand(this, Util);
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

    public InputVM(IUtilitiesService util, MultiplexVM multiplex)
    {
        Util = util;
        Multiplex = multiplex;
        SourceFiles.Add(multiplex.SourceFile);

        CreateTracks(SourceFiles.First(s => s.IsPrimary));
    }

    public async void CreateTracks(SourceFileInfo sourceFile)
    {
        ProcessResult pr = await Util.ExLib.Run(ProcessResultNames.MKVMergeJ, sourceFile);
        ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
        MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(pr.StdOutput);
        sourceFile.Type = result.Container.Type;
        foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "video"))
        {
            Tracks.Add(new TrackVM(this, sourceFile, track, TrackPropertiesTypes.Video, Util));
        }
        foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "audio"))
        {
            Tracks.Add(new TrackVM(this, sourceFile, track, TrackPropertiesTypes.Audio, Util));
        }
        foreach (MKVMergeJ.Track track in result.Tracks.Where(t => t.Type == "subtitles"))
        {
            Tracks.Add(new TrackVM(this, sourceFile, track, TrackPropertiesTypes.Subtitles, Util));
        }
        foreach (MKVMergeJ.Attachment attachment in result.Attachments)
        {
            Attachment att = new(Util, sourceFile, Multiplex.Attachments, attachment);
            Multiplex.Attachments.ExistingAttachments.Add(att);
        }
        Multiplex.Output.Output.Title = result.Container.Properties.Title;
        //TODO Chapters
    }
}
