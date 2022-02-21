using MKVStudio.Services;

namespace MKVStudio.Models;

public class Output : BaseModel
{
    private readonly IUtilitiesService _util;

    public string Title { get; set; }

    public Output(IUtilitiesService util)
    {
        _util = util;
    }
}
