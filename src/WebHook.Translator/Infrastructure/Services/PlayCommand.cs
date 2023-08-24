using Telegram.Bot;
using WebHook.Translator.Infrastructure.Services.Interfaces;
using WebHook.Translator.Models;
using WebHook.Translator.Utils;

namespace WebHook.Translator.Infrastructure.Services;

public class PlayCommand : ICommand
{
    public static string CommandName => "/play";

    public string GetCommandName()
        => CommandName;

    public async Task HandleCommand(long chatId, ITelegramBotClient botClient, string? arguments = null, CancellationToken cancellationToken = default)
    {
        int messageId = (await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Processing...",
            cancellationToken: cancellationToken)).MessageId;

        string message = "choose play";

        //var markups = Utilities.ParsePlayCollectionKeyboardMarkup(
        //    plays: new Play[] { },
        //    _re);

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "test play",
            cancellationToken: cancellationToken);
    }
}
