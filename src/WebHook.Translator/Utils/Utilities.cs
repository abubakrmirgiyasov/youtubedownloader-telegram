using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;
using WebHook.Translator.Common;
using WebHook.Translator.Models;

namespace WebHook.Translator.Utils;

public static partial class Utilities
{
    public static InlineKeyboardMarkup ParseLanguageCollectionKeyboardMarkup(
        IEnumerable<Language> languages,
        int columns,
        int messageId,
        KeyboardDirection direction,
        JsonSerializerOptions jsonSerializerOptions)
    {
        var buttonList = new List<IEnumerable<InlineKeyboardButton>>();
        var row = new List<InlineKeyboardButton>();

        foreach (var language in languages)
        {
            var callbackData = new LanguageChoiseResponse
            {
                Code = language.Code,
                Direction = direction,
                MessageId = messageId,
            };

            row.Add(InlineKeyboardButton.WithCallbackData(
                text: language.ToString(),
                callbackData: JsonSerializer.Serialize(
                    value: callbackData,
                    options: jsonSerializerOptions)));

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
