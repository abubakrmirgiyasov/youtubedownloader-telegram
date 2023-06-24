using Telegram.Bot;

namespace WebHook.YouTube.Downloader.Services.Commands;

public class StartCommand : ICommand
{
    public static string CommandName => "/start";

    public string GetCommandName()
    {
        return CommandName;
    }

    public Task HandleCommand(long chatId, ITelegramBotClient botClient, CancellationToken cancellationToken, string? arguments = null)
    {
        var message = "Welcome man";

        return botClient.SendTextMessageAsync(
            chatId: chatId,
            text: message,
            cancellationToken: cancellationToken);
    }
}
