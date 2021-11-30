namespace MKVStudio.Services
{
    public interface IRegistryService
    {
        string GetFFmpeg();
        string GetMKVInfo();
        string GetMKVMerge();
        string GetMKVPropEdit();
        string GetMKVExtract();
    }
}