using MKVStudio.Models;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IFfmpegService
    {
        Task<ProcessResult> RunFFMPEG(string arguments, string processName);
    }
}