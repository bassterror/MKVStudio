namespace MKVStudio.Models;

public class MKVMergeJ
{
    public Attachment[] Attachments { get; set; }
    public Chapter[] Chapters { get; set; }
    public Containers Container { get; set; }
    public Global_Tag[] Global_tags { get; set; }
    public Track_Tag[] Track_tags { get; set; }
    public Track[] Tracks { get; set; }

    public class Attachment
    {
        public string Content_type { get; set; }
        public string Description { get; set; }
        public string File_name { get; set; }
        public int Id { get; set; }
        public AttProperties Properties { get; set; }
        public int Size { get; set; }
    }

    public class AttProperties
    {
        public string UID { get; set; }
    }

    public class Chapter
    {
        public int Num_entries { get; set; }
    }

    public class Containers
    {
        public ContProperties Properties { get; set; }
        public bool Recognized { get; set; }
        public bool Supported { get; set; }
        public string Type { get; set; }
    }

    public class ContProperties
    {
        public string Muxing_application { get; set; }
        public string Segment_uid { get; set; }
        public string Title { get; set; }
        public string Writing_application { get; set; }
    }

    public class Global_Tag
    {
        public int Num_entries { get; set; }
    }

    public class Track_Tag
    {
        public int Num_entries { get; set; }
        public int Track_id { get; set; }
    }

    public class Track
    {
        public string Codec { get; set; }
        public int ID { get; set; }
        public TrProperties Properties { get; set; }
        public string Type { get; set; }
    }

    public class TrProperties
    {
        public string Codec_id { get; set; }
        public bool Default_track { get; set; }
        public string Display_dimensions { get; set; }
        public bool Enabled_track { get; set; }
        public bool Forced_track { get; set; }
        public bool Flag_hearing_impaired { get; set; }
        public bool Flag_original { get; set; }
        public bool Flag_visual_impaired { get; set; }
        public bool Flag_commentary { get; set; }
        public string Language { get; set; }
        public int Number { get; set; }
        public string Packetizer { get; set; }
        public string Pixel_dimensions { get; set; }
        public string UID { get; set; }
        public int Audio_channels { get; set; }
        public int Audio_sampling_frequency { get; set; }
        public string Encoding { get; set; }
        public string Language_ietf { get; set; }
        public string Track_name { get; set; }
        public string Content_encoding_algorithms { get; set; }
    }
}
