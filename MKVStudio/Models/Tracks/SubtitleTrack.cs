namespace MKVStudio.Models
{
    public class SubtitleTrack
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string UID { get; set; }
        public string CodecId { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagHearingImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public string Encoding { get; set; }
        public string Language { get; set; }
        public string LanguageIETF { get; set; }
        public int Number { get; set; }
        public string Packetizer { get; set; }
        public string ContentEncodingAlgorithms { get; set; }
    }
}
