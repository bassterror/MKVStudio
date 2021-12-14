using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AudioEditVM : BaseViewModel
    {
        public MultiplexVM SelectedVideo { get; set; }

        #region LoudnormFirstPass
        public string InputI { get; set; }
        public string InputTP { get; set; }
        public string InputLRA { get; set; }
        public string InputTresh { get; set; }
        public string OutputTP { get; set; }
        public string OutputLRA { get; set; }
        public string OutputTresh { get; set; }
        public string NormalizationType { get; set; }
        public string TargetOffset { get; set; }
        #endregion

        public ICommand RunLoudnormFirstPassCommand { get; set; }

        public AudioEditVM(MultiplexVM selectedVideo, IExternalLibrariesService externalLibrariesService)
        {
            SelectedVideo = selectedVideo;
            RunLoudnormFirstPassCommand = new RunLoudnormFirstPassCommand(SelectedVideo, this, externalLibrariesService);
        }
    }
}
