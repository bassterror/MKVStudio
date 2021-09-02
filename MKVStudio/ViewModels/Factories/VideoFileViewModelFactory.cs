using MKVStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKVStudio.ViewModels.Factories
{
    public class VideoFileViewModelFactory : IViewModelFactory<VideoFileViewModel>
    {
        private Video _selectedVideo;

        public VideoFileViewModelFactory()
        {
            
        }

        public VideoFileViewModel CreateViewModel()
        {
            return new VideoFileViewModel();
        }
    }
}
