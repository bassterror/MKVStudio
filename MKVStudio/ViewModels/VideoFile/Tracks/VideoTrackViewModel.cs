using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;

namespace MKVStudio.ViewModels
{
    public class VideoTrackViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; }
        public IExternalLibrariesService ExLib { get; }

        public string Name { get; set; }
        public string ID { get; set; }
        public string UID { get; set; }
        public string Codec { get; set; }
        public string CodecId { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagHearingImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public Language Language { get; set; }
        public string LanguageIETF { get; set; }
        public int Number { get; set; }
        public string Packetizer { get; set; }
        public string DisplayDimensions { get; set; }
        public string PixelDimensions { get; set; }

        public VideoTrackViewModel(VideoFileViewModel videoFileViewModel, MKVMergeJ.Track track = null, IExternalLibrariesService externalLibrariesService = null)
        {
            SelectedVideo = videoFileViewModel;
            if (track != null)
            {
                ExLib = externalLibrariesService;
                Name = track.Properties.Track_name;
                ID = track.ID.ToString();
                UID = track.Properties.UID;
                Codec = track.Codec;
                CodecId = track.Properties.Codec_id;
                DefaultTrack = track.Properties.Default_track;
                EnabledTrack = track.Properties.Enabled_track;
                ForcedTrack = track.Properties.Forced_track;
                FlagHearingImpaired = track.Properties.Flag_hearing_impaired;
                FlagCommentary = track.Properties.Flag_commentary;
                Language = string.IsNullOrWhiteSpace(track.Properties.Language) ? ExLib.Languages.First(a => a.ID == "und") : ExLib.Languages.First(a => a.ID == track.Properties.Language);
                LanguageIETF = track.Properties.Language_ietf;
                Number = track.Properties.Number;
                Packetizer = track.Properties.Packetizer;
                DisplayDimensions = track.Properties.Display_dimensions;
                PixelDimensions = track.Properties.Pixel_dimensions; 
            }
        }

    }
}
