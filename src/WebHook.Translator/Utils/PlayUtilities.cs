using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;
using WebHook.Translator.Common;
using WebHook.Translator.Models;

namespace WebHook.Translator.Utils;

public static partial class Utilities
{
    public static InlineKeyboardMarkup ParsePlayCollectionKeyboardMarkup(
        IEnumerable<Play> plays,
        int columns,
        int messageId,
        KeyboardDirection direction,
        JsonSerializerOptions jsonSerializerOptions)
    {
        var buttonList = new List<IEnumerable<InlineKeyboardButton>>();
        var row = new List<InlineKeyboardButton>();

        foreach (var play in plays)
        {
            var callbackData = new ChoiceResponse()
            {
                Code = play.Code,
                Direction = direction,
                MessageId = messageId,
                MarkupType = MarkupType.Play,
            };

            string json = JsonSerializer.Serialize(
                value: callbackData,
                options: jsonSerializerOptions);

            row.Add(InlineKeyboardButton.WithCallbackData(
                text: play.ToString(),
                callbackData: json));

            if (row.Count == columns)
            {
                buttonList.Add(row);
                row.Clear();
            }
        }

        if (row.Count > 0)
            buttonList.Add(row.ToArray());
        
        return new InlineKeyboardMarkup(buttonList);
    }
}
