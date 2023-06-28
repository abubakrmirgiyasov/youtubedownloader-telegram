#nullable disable

using WebHook.YouTube.Downloader.Common;
using Type = WebHook.YouTube.Downloader.Common.Type;

namespace WebHook.YouTube.Downloader.Models;

public class Download
{
    public string Url { get; set; }

    public string Extension { get; set; }

    public string AuthorLink { get; set; }

    public string Title { get; set; }

    public string Name { get; set; }

    public string Ico { get; set; }

    public Format Format { get; set; }

    public Type Type { get; set; }

    public DateTimeOffset DatePublish { get; set; }

    public override string ToString()
    {
        return $"{Ico} {Format}p";
    }
}
