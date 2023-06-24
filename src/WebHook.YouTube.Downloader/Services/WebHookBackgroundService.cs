using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace WebHook.YouTube.Downloader.Services;

public class WebHookBackgroundService : IHostedService
{
    private readonly ILogger<WebHookBackgroundService> _logger;
    private readonly IOptions<AppSettings> _settings;
    private readonly IServiceProvider _service;

    private ITelegramBotClient _botClient = default!;

    public WebHookBackgroundService(
        IServiceProvider service, 
        IOptions<AppSettings> settings, 
        ILogger<WebHookBackgroundService> logger)
    {
        _logger = logger;
        _service = service;
        _settings = settings;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _service.CreateScope();
            _botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            string url = $"{_settings.Value.TelegramSettings.HostAddress}/{_settings.Value.TelegramSettings.Route}";

            await _botClient.SetWebhookAsync(
                url: url,
                secretToken: _settings.Value.TelegramSettings.SecretToken,
                cancellationToken: cancellationToken,
                allowedUpdates: new[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
                    UpdateType.CallbackQuery,
                    UpdateType.Unknown,
                });

            _logger.LogInformation("[POST] Successfully setted Telegram webhook");
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Failed to set Telegram webhook: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("[POST] Successfully unsetted Telegram webhook");
        }
        catch (Exception ex)
        {
            _logger.LogCritical("[POST] Failed to unset Telegram webhook: {ErrorMessage}", ex.Message);
        }
    }
}
