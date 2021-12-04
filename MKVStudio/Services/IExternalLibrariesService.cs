using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IExternalLibrariesService
    {
        IUtilitiesService Util { get; set; }
        Task<ProcessResult> Run(VideoFileViewModel video, ProcessResultNames processName);
    }
}