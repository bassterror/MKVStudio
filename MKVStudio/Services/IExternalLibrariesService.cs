using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IExternalLibrariesService
    {
        IUtilitiesService Util { get; set; }
        ObservableCollection<Language> AllLanguages { get; set; }
        ObservableCollection<Language> Languages { get; set; }
        Task<ProcessResult> Run(ProcessResultNames processName, VideoFileViewModel video = null);
    }
}