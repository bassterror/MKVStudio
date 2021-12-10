using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AudioTrackVM : BaseViewModel
    {
        public IExternalLibrariesService ExLib { get; }

        public string Name { get; set; }
        public string ID { get; set; }
        public string UID { get; set; }
        public string Codec { get; set; }
        public string CodecId { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagVisualImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public int Channels { get; set; }
        public int SampleRate { get; set; }
        public Language Language { get; set; }
        public string LanguageIETF { get; set; }
        public int Number { get; set; }
        public ICommand RemoveTrack { get; set; }

        public AudioTrackVM(IExternalLibrariesService exLib, TracksVM tracksVM, MKVMergeJ.Track track = null)
        {
            ExLib = exLib;
            RemoveTrack = new RemoveTrackCommand(tracksVM, null, this, null);
            if (track != null)
            {
                Name = track.Properties.Track_name;
                ID = track.ID.ToString();
                UID = track.Properties.UID;
                Codec = track.Codec;
                CodecId = track.Properties.Codec_id;
                DefaultTrack = track.Properties.Default_track;
                EnabledTrack = track.Properties.Enabled_track;
                ForcedTrack = track.Properties.Forced_track;
                FlagVisualImpaired = track.Properties.Flag_visual_impaired;
                FlagCommentary = track.Properties.Flag_commentary;
                Channels = track.Properties.Audio_channels;
                SampleRate = track.Properties.Audio_sampling_frequency;
                Language = string.IsNullOrWhiteSpace(track.Properties.Language) ? ExLib.Languages.First(a => a.ID == "und") : ExLib.Languages.First(a => a.ID == track.Properties.Language);
                LanguageIETF = track.Properties.Language_ietf;
                Number = track.Properties.Number;
            }
        }
    }
}
