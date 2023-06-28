using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;
using WebHook.YouTube.Downloader.Common;
using WebHook.YouTube.Downloader.Models;

namespace WebHook.YouTube.Downloader.Utils;

public static class TelegramUtilities
{
    public static InlineKeyboardMarkup ParseDownloadCollectionKeyboardMarkup(
        int columns,
        int messageId,
        KeyboardDirection direction,
        IEnumerable<Download> collection,
        JsonSerializerOptions jsonSerializerOptions)
    {
        var buttonList = new List<IEnumerable<InlineKeyboardButton>>();

        var row = new List<InlineKeyboardButton>();

        foreach (var item in collection)
        {
            var callbackData = new DownloadChoiseResponse()
            {
                Ico = item.Ico,
                MessageId = messageId,
                Direction = direction,
            };

            row.Add(InlineKeyboardButton.WithCallbackData(
                text: item.ToString(),
                callbackData: JsonSerializer.Serialize(callbackData, jsonSerializerOptions)));

            if (row.Count == columns)
            {
                buttonList.Add(row.ToArray());
                row.Clear();
            }
        }

        if (row.Count > 0)
            buttonList.Add(row.ToArray());

        return new InlineKeyboardMarkup(buttonList);
    }
}
