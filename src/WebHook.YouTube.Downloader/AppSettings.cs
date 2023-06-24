#nullable disable

using WebHook.YouTube.Downloader.Services;

namespace WebHook.YouTube.Downloader;

public class AppSettings
{
    public MongoDbSettings MongoDbSettings { get; set; }

    public TelegramSettings TelegramSettings { get; set; }
}

public class MongoDbSettings
{
    public string DatabaseName { get; set; }

    public string ConnectionString { get; set; }
}

public class TelegramSettings : ConfigurationBase
{
    public static readonly string SectionPath = nameof(TelegramSettings);

    public string BotToken { get; set; }

    public string Route { get; set; }

    public string HostAddress { get; set; }

    public string SecretToken { get; set; }

    public override void Validate()
    {
        ValidateNotNull(SectionPath, nameof(BotToken), BotToken);
        ValidateNotNull(SectionPath, nameof(Route), Route);
        ValidateNotNull(SectionPath, nameof(HostAddress), HostAddress);
        ValidateNotNull(SectionPath, nameof(SecretToken), SecretToken);
    }
} 
