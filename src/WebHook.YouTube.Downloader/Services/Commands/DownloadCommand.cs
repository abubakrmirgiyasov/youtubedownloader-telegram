using System.Text.Json;
using Telegram.Bot;
using WebHook.YouTube.Downloader.Common;
using WebHook.YouTube.Downloader.Utils;

namespace WebHook.YouTube.Downloader.Services.Commands;

public class DownloadCommand : ICommand
{
    public static string CommandName => "/download";

    private readonly IFormatManager _format;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly int _replyKeyboardColumns;

    public DownloadCommand(
        IFormatManager format, 
        JsonSerializerOptions jsonSerializerOptions, 
        int replyKeyboardColumns)
    {
        _format = format;
        _jsonSerializerOptions = jsonSerializerOptions;
        _replyKeyboardColumns = replyKeyboardColumns;
    }

    public string GetCommandName()
    {
        return CommandName;
    }

    public async Task HandleCommand(long chatId, ITelegramBotClient botClient, CancellationToken cancellationToken, string? arguments = null)
    {
        var messageId = (await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Processing...",
            cancellationToken: cancellationToken)).MessageId;

        var collection = await _format.GetDownloadsFormatCollectionAsync();

        var replyMarkup = TelegramUtilities.ParseDownloadCollectionKeyboardMarkup(
            columns: _replyKeyboardColumns,
            messageId: messageId,
            direction: KeyboardDirection.Source,
            collection: collection,
            jsonSerializerOptions: _jsonSerializerOptions);

        await botClient.EditMessageTextAsync(
            chatId: chatId,
            messageId: messageId,
            text: "Change me, after test",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }
}
