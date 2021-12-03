using MKVStudio.Models;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IExternalLibrariesService
    {
        IUtilitiesService Util { get; set; }
        Task<ProcessResult> Run(VideoFile video, ProcessResultNames processName);
    }
}