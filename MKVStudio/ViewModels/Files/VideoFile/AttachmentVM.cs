using MKVStudio.Models;

namespace MKVStudio.ViewModels
{
    public class AttachmentVM
    {
        public VideoFileVM SelectedVideo { get; }

        public string ID { get; set; }
        public string UID { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }

        public AttachmentVM(VideoFileVM videoFileVM, MKVMergeJ.Attachment attachment = null)
        {
            if (attachment != null)
            {
                SelectedVideo = videoFileVM;
                ID = attachment.Id.ToString();
                UID = attachment.Properties.UID;
                ContentType = attachment.Content_type;
                Name = attachment.File_name;
                Description = attachment.Description;
                Size = attachment.Size.ToString(); 
            }
        }
    }
}
