using MKVStudio.ViewModels.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MKVStudio.State.VideosNavigator
{
    public enum VideosViewModelType
    {
        General,
        MediaInfo,
        Convert
    }

    public interface IVideosNavigator
    {
        BaseVideosViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentVideosViewModelCommand { get; }
    }
}
