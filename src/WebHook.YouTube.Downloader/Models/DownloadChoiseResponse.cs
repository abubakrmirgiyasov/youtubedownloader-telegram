#nullable disable

using WebHook.YouTube.Downloader.Common;

namespace WebHook.YouTube.Downloader.Models;

public class DownloadChoiseResponse
{
    public int MessageId { get; set; }
    
    public string Ico { get; set; }

    public KeyboardDirection Direction { get; set; }
}
