using System;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("5534091527:AAFGXSqAZIXKbPQ5dixSnYbVxUqiLfcREsw");

using var cancellationToken = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions()
{ 
    AllowedUpdates = Array.Empty<UpdateType>(), 
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cancellationToken.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cancellationToken.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine("Received a '{0}' message in chat {1}.", messageText, chatId);

    if (message.Entities != null)
    {
        foreach (var entity in message.Entities)
        {
            if (entity.Type == MessageEntityType.BotCommand)
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said\n" + messageText,
                    cancellationToken: cancellationToken);
            }
            else if (entity.Type == MessageEntityType.Url)
            {
                Console.WriteLine("youtube? {0}", GetUrl(messageText) ? "yes" : "no");

                if (GetUrl(messageText))
                {
                    //var sentMessage = await botClient.SendVideoAsync(
                    //    chatId: chatId,
                    //    video: messageText,
                    //    caption: messageText,
                    //    //       supportsStreaming: true,
                    //    cancellationToken: cancellationToken);
                }
            }
            else if (entity.Type == MessageEntityType.Mention)
            {

            }
            else if (entity.Type == MessageEntityType.PhoneNumber)
            {
                var messageSent = await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
            }
            else
            {
                var messageId = message.MessageId;
                await botClient.DeleteMessageAsync(
                    chatId: chatId,
                    messageId: messageId,
                     cancellationToken: cancellationToken);
            }
        }
    }
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var errorMessage = exception switch
    {
        ApiRequestException ex =>
            $"Telegram API Error: \n[{ex.ErrorCode}]" +
            $"\n{ex.Message}",
        _=>exception.ToString(),
    };
    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}

bool GetUrl(string url)
{
    try
    {
        var client = new HttpClient() { BaseAddress = new Uri(url), };
        return client.BaseAddress.Host.Contains("youtube.com");
    }
    catch (UriFormatException)
    {
        url = "https://" + url;
        var client = new HttpClient() { BaseAddress = new Uri(url), };
        return client.BaseAddress.Host.Contains("youtube.com");

    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message, ex);
    }
}
