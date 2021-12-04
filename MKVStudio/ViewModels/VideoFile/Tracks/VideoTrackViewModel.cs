using MKVStudio.Models;

namespace MKVStudio.ViewModels
{
    public class VideoTrackViewModel
    {
        public VideoFileViewModel SelectedVideo { get; }

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
        public string Language { get; set; }
        public string LanguageIETF { get; set; }
        public int Number { get; set; }
        public string Packetizer { get; set; }
        public string DisplayDimensions { get; set; }
        public string PixelDimensions { get; set; }

        public VideoTrackViewModel(VideoFileViewModel videoFileViewModel, MKVMergeJ.Track videoTrack)
        {
            SelectedVideo = videoFileViewModel;
            Name = videoTrack.Properties.Track_name;
            ID = videoTrack.ID.ToString();
            UID = videoTrack.Properties.UID;
            Codec = videoTrack.Codec;
            CodecId = videoTrack.Properties.Codec_id;
            DefaultTrack = videoTrack.Properties.Default_track;
            EnabledTrack = videoTrack.Properties.Enabled_track;
            ForcedTrack = videoTrack.Properties.Forced_track;
            FlagHearingImpaired = videoTrack.Properties.Flag_hearing_impaired;
            FlagCommentary = videoTrack.Properties.Flag_commentary;
            Language = videoTrack.Properties.Language;
            LanguageIETF = videoTrack.Properties.Language_ietf;
            Number = videoTrack.Properties.Number;
            Packetizer = videoTrack.Properties.Packetizer;
            DisplayDimensions = videoTrack.Properties.Display_dimensions;
            PixelDimensions = videoTrack.Properties.Pixel_dimensions;
        }

    }
}
