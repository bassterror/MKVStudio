using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;

namespace MKVStudio.ViewModels;

public class TrackVM : BaseViewModel
{
    public InputVM Input { get; }
    public SourceFileInfo SourceFile { get; }

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

    public TrackVM(InputVM input, SourceFileInfo sourceFile, MKVMergeJ.Track mkvMergeTrack, TrackPropertiesTypes type, IUtilitiesService util)
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
                Codec = vProp.VideoProperties.Codec;
                Language = vProp.VideoProperties.Language.Name;
                Name = vProp.VideoProperties.Name;
                break;
            case TrackPropertiesTypes.Audio:
                AudioPropertiesVM aProp = new(util, mkvMergeTrack);
                Properties = aProp;
                Codec = aProp.AudioProperties.Codec;
                Language = aProp.AudioProperties.Language.Name;
                Name = aProp.AudioProperties.Name;
                break;
            case TrackPropertiesTypes.Subtitles:
                SubtitlesPropertiesVM sProp = new(util, mkvMergeTrack);
                Properties = sProp;
                Codec = sProp.SubtitlesProperties.Codec;
                Language = sProp.SubtitlesProperties.Language.Name;
                Name = sProp.SubtitlesProperties.Name;
                break;
        }
    }

}
