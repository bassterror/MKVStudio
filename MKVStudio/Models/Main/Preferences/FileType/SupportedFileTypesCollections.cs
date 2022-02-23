using MKVStudio.Services;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MKVStudio.Models;

public class SupportedFileTypesCollections
{
    private readonly IUtilitiesService _util;

    public List<SupportedFileType> SupportedFileTypes { get; private set; } = new();
    public List<SupportedFileType> AttachmentFileTypes { get; private set; } = new();

    public SupportedFileTypesCollections(IUtilitiesService util)
    {
        _util = util;
        GetSupportedFileTypes();
        AttachmentFileTypes.Add(new SupportedFileType("Images", "*.jpg;*.jpeg;*.png;*.bmp;*.gif"));
        AttachmentFileTypes.Add(new SupportedFileType("Fonts", "*.otf;*.ttf;*.ttc"));
        AttachmentFileTypes.Add(new SupportedFileType("Text", "*.txt;*.doc;*.docx;*.pdf"));
        AttachmentFileTypes.Add(new SupportedFileType("JSON", "*.json"));
        AttachmentFileTypes.Add(new SupportedFileType("CSV", "*.csv"));
    }

    private async void GetSupportedFileTypes()
    {
        Dictionary<string, string> uniqueExts = new();
        string exts = string.Empty;
        ProcessResult pr = await _util.ExLib.RunProcess(ProcessResultNames.MKVMergeSupportedFileTypes);
        string[] lines = pr.StdOutput.Split("\r\n");
        for (int i = 2; i < lines.Length - 1; i++)
        {
            Match match = Regex.Match(lines[i], @"^\s\s(.+)\s\[(.+)\]$");
            SupportedFileType fileType = new(match);
            SupportedFileTypes.Add(fileType);
            foreach (string ext in fileType.Extensions.Split(";"))
            {
                _ = uniqueExts.TryAdd(ext, ext);
            }
        }
        foreach (string extension in uniqueExts.Keys)
        {
            exts += $"{extension};";
        }
        SupportedFileType allSupportedFileTypes = new("All supported media files", exts.Remove(exts.Length - 1, 1));
        SupportedFileTypes.Insert(0, allSupportedFileTypes);
    }

    public string CreateFiltersAllSuported()
    {
        string filters = string.Empty;
        foreach (SupportedFileType fileType in SupportedFileTypes)
        {
            filters += $"{fileType.Name}|{fileType.Extensions}|";
        }

        return filters.Remove(filters.Length - 1, 1);
    }

    public string CreateFiltersAllSuportedOnlyExt()
    {
        string filters = string.Empty;
        Dictionary<string, string> uniqueExts = new();
        foreach (SupportedFileType fileType in SupportedFileTypes)
        {
            foreach (string ext in fileType.Extensions.Split(";"))
            {
                _ = uniqueExts.TryAdd(ext, ext);
            }
        }
        foreach (string extension in uniqueExts.Keys)
        {
            filters += $"{extension}|";
        }

        return filters.Remove(filters.Length - 1, 1);
    }

    public string CreateFiltersAllAttachments()
    {
        string filters = string.Empty;
        foreach (SupportedFileType fileType in AttachmentFileTypes)
        {
            filters += $"{fileType.Name}|{fileType.Extensions}|";
        }

        return filters.Remove(filters.Length - 1, 1);
    }

    public string CreateFiltersAllAttachmentsOnlyExt()
    {
        string filters = string.Empty;
        Dictionary<string, string> uniqueExts = new();
        foreach (SupportedFileType fileType in AttachmentFileTypes)
        {
            foreach (string ext in fileType.Extensions.Split(";"))
            {
                _ = uniqueExts.TryAdd(ext, ext);
            }
        }
        foreach (string extension in uniqueExts.Keys)
        {
            filters += $"{extension}|";
        }

        return filters.Remove(filters.Length - 1, 1);
    }
}
