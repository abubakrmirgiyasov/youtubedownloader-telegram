using Telegram.Bot;

namespace WebHook.YouTube.Downloader.Services.Commands;

public interface ICommand
{
    public static virtual string CommandName { get; set; } = default!;

    public string GetCommandName();

    public Task HandleCommand(
        long chatId,
        ITelegramBotClient botClient,
        CancellationToken cancellationToken,
        string? arguments = null);
}
