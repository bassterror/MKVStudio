using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IExternalLibrariesService
    {
        IUtilitiesService Util { get; set; }
        Dictionary<string, Language> Languages { get; set; }
        Task<ProcessResult> Run(ProcessResultNames processName, VideoFileViewModel video = null);
    }
}