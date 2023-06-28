using WebHook.YouTube.Downloader.Models;

namespace WebHook.YouTube.Downloader.Common;

public enum SizeUnits
{
    MB
}

public enum Type
{
    Video,
    Audio
}

public enum Format : int
{
    Low = 144,
    LowMedium = 360,
    Medium = 480,
    HighMedium = 720,
    High = 1080
}

public enum KeyboardDirection : byte
{
    Source,
    Target
}

public interface IFormatManager
{
    public Task<IEnumerable<Download>> GetDownloadsFormatCollectionAsync();

    public Task<Download> GetDownloadByFormatAsync(Format format);
}

public class FormatManager : IFormatManager
{
    private readonly Download[] _formats =
    {
        new()
        {
            Name = "1080",
            Format = Format.High,
            Ico = "🎥",
        },
        new()
        {
            Name = "720",
            Format = Format.HighMedium,
            Ico = "🎥",
        },
        new()
        {
            Name = "480",
            Format = Format.HighMedium,
            Ico = "🎥",
        },
        new()
        {
            Name = "360",
            Format = Format.HighMedium,
            Ico = "🎥",
        },
        new()
        {
            Name = "144",
            Format = Format.HighMedium,
            Ico = "🎥",
        },
    };

    public Task<Download> GetDownloadByFormatAsync(Format format)
    {
        return Task.FromResult(_formats.Single(x => x.Format == format));
    }

    public Task<IEnumerable<Download>> GetDownloadsFormatCollectionAsync()
    {
        return Task.FromResult<IEnumerable<Download>>(_formats);
    }
}
