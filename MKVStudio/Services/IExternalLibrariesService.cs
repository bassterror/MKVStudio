using MKVStudio.Models;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IExternalLibrariesService
    {
        IUtilitiesService Util { get; set; }
        Task<ProcessResult> RunProcess(UtilitiesService.Executables executable, string arguments, ProcessResultNames processName);
    }
}