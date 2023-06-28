using WebHook.YouTube.Downloader.Common;

namespace WebHook.YouTube.Downloader.Extensions;

public static class ConvertByteExtension
{
    public static string? ToSize(this long? value, SizeUnits units)
    {
        return value is not null 
            ? (Math.Round((double)(value / (double)Math.Pow(1024, (long)units))), 2).ToString()
            : "";
    }
}
