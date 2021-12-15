using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class TrackVM : BaseViewModel
    {
        public InputVM Input { get; }
        public bool IsChecked { get; set; }
        public TrackPropertiesTypes Type { get; }
        public TrackProperties Properties { get; set; }
        public string Codec { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        public TrackVM(InputVM input, MKVMergeJ.Track mkvMergeTrack, TrackPropertiesTypes type, IExternalLibrariesService exLib)
        {
            Input = input;
            IsChecked = true;
            Type = type;
            switch (Type)
            {
                case TrackPropertiesTypes.Video:
                    VideoPropertiesVM vProp = new(exLib, mkvMergeTrack);
                    Properties = vProp;
                    Codec = vProp.Codec;
                    Language = vProp.Language.Name;
                    Name = vProp.Name;
                    break;
                case TrackPropertiesTypes.Audio:
                    AudioPropertiesVM aProp = new(exLib, mkvMergeTrack);
                    Properties = aProp;
                    Codec = aProp.Codec;
                    Language = aProp.Language.Name;
                    Name = aProp.Name;
                    break;
                case TrackPropertiesTypes.Subtitles:
                    SubtitlesPropertiesVM sProp = new(exLib, mkvMergeTrack);
                    Properties = sProp;
                    Codec = sProp.Codec;
                    Language = sProp.Language.Name;
                    Name = sProp.Name;
                    break;
            }
        }

    }
}
