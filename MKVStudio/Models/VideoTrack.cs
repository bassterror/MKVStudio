namespace MKVStudio.Models
{
    public class VideoTrack
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string UID { get; set; }
        public string CodecId { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public string Language { get; set; }
        public string LanguageIETF { get; set; }
        public int Number { get; set; }
        public string Packetizer { get; set; }
        public string DisplayDimensions { get; set; }
        public string PixelDimensions { get; set; }
    }
}
