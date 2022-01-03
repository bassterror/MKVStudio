using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;

namespace MKVStudio.ViewModels;

public class TrackVM : BaseViewModel
{
    public InputVM Input { get; }
    public SourceFileVM SourceFile { get; }

    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            Input.IsCheckAllT = Input.Tracks.Where(t => t.IsChecked).Count() != Input.Tracks.Count;
            Input.IsUncheckAllT = Input.Tracks.Where(t => !t.IsChecked).Count() != Input.Tracks.Count;
        }
    }
    public TrackPropertiesTypes Type { get; }
    public TrackProperties Properties { get; set; }
    public string Codec { get; set; }
    public string Language { get; set; }
    public string Name { get; set; }

    public TrackVM(InputVM input, SourceFileVM sourceFile, MKVMergeJ.Track mkvMergeTrack, TrackPropertiesTypes type, IUtilitiesService util)
    {
        Input = input;
        SourceFile = sourceFile;
        IsChecked = true;
        Type = type;
        switch (Type)
        {
            case TrackPropertiesTypes.Video:
                VideoPropertiesVM vProp = new(util, mkvMergeTrack);
                Properties = vProp;
                Codec = vProp.Codec;
                Language = vProp.Language.Name;
                Name = vProp.Name;
                break;
            case TrackPropertiesTypes.Audio:
                AudioPropertiesVM aProp = new(util, mkvMergeTrack);
                Properties = aProp;
                Codec = aProp.Codec;
                Language = aProp.Language.Name;
                Name = aProp.Name;
                break;
            case TrackPropertiesTypes.Subtitles:
                SubtitlesPropertiesVM sProp = new(util, mkvMergeTrack);
                Properties = sProp;
                Codec = sProp.Codec;
                Language = sProp.Language.Name;
                Name = sProp.Name;
                break;
        }
    }

}
