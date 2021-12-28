using System.Collections.Generic;

namespace MKVStudio.Models
{
    public class SupportedFileTypesCollection
    {
        public List<SupportedFileType> SupportedFileTypes { get; set; } = new();
        public List<SupportedFileType> AttachmentFileTypes { get; set; } = new();

        public SupportedFileTypesCollection()
        {
            AttachmentFileTypes.Add(new SupportedFileType("Images", "*.jpg;*.jpeg;*.png;*.bmp;*.gif"));
            AttachmentFileTypes.Add(new SupportedFileType("Fonts", "*.otf;*.ttf;*.ttc"));
            AttachmentFileTypes.Add(new SupportedFileType("Text", "*.txt;*.doc;*.docx;*.pdf"));
            AttachmentFileTypes.Add(new SupportedFileType("JSON", "*.json"));
            AttachmentFileTypes.Add(new SupportedFileType("CSV", "*.csv"));
        }

        public string CreateFiltersAllSuported()
        {
            string filters = string.Empty;
            foreach (SupportedFileType fileType in SupportedFileTypes)
            {
                filters += $"{fileType.Name}|{fileType.Extensions}|";
            }

            return filters.Remove(filters.Length - 1, 1);
        }

        public string CreateFiltersAllSuportedOnlyExt()
        {
            string filters = string.Empty;
            Dictionary<string, string> uniqueExts = new();
            foreach (SupportedFileType fileType in SupportedFileTypes)
            {
                foreach (string ext in fileType.Extensions.Split(";"))
                {
                    _ = uniqueExts.TryAdd(ext, ext);
                }
            }
            foreach (string extension in uniqueExts.Keys)
            {
                filters += $"{extension}|";
            }

            return filters.Remove(filters.Length - 1, 1);
        }

        public string CreateFiltersAllAttachments()
        {
            string filters = string.Empty;
            foreach (SupportedFileType fileType in AttachmentFileTypes)
            {
                filters += $"{fileType.Name}|{fileType.Extensions}|";
            }

            return filters.Remove(filters.Length - 1, 1);
        }

        public string CreateFiltersAllAttachmentsOnlyExt()
        {
            string filters = string.Empty;
            Dictionary<string, string> uniqueExts = new();
            foreach (SupportedFileType fileType in AttachmentFileTypes)
            {
                foreach (string ext in fileType.Extensions.Split(";"))
                {
                    _ = uniqueExts.TryAdd(ext, ext);
                }
            }
            foreach (string extension in uniqueExts.Keys)
            {
                filters += $"{extension}|";
            }

            return filters.Remove(filters.Length - 1, 1);
        }
    }
}
