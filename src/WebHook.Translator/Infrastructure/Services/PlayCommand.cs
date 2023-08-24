using Telegram.Bot;
using WebHook.Translator.Infrastructure.Services.Interfaces;

namespace WebHook.Translator.Infrastructure.Services;

public class PlayCommand : ICommand
{
    public static string CommandName => "/play";

    public string GetCommandName()
        => CommandName;

    public async Task HandleCommand(long chatId, ITelegramBotClient botClient, string? arguments = null, CancellationToken cancellationToken = default)
    {
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "test play",
            cancellationToken: cancellationToken);
    }
}
