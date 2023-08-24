using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using WebHook.Translator.Common;
using WebHook.Translator.Infrastructure.Services.Interfaces;
using WebHook.Translator.Models;
using WebHook.Translator.Services;

namespace WebHook.Translator.Infrastructure;

public class UpdateHandlerServiceImplementation : UpdateHandlerService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly CommandManager _command;
    private readonly ILanguageManager _languageManager;

    public UpdateHandlerServiceImplementation(
        JsonSerializerOptions jsonSerializerOptions,
        CommandManager command,
        ILanguageManager languageManager,
        ITelegramBotClient botClient) 
        : base(botClient)
    {
        _command = command;
        _languageManager = languageManager;
        _jsonSerializerOptions = jsonSerializerOptions;

        MessageReceived += OnMessageReceived;
        CallBackQuery += OnCallbackQueryData;
    }

    private async Task OnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        try
        {
            long chatId = message.Chat.Id;
            
            if (message.Text!.StartsWith('/'))
            {
                await _command.HandleCommandAsync(
                    chatId:  chatId,
                    command: message.Text,
                    botClient: _BotClient,
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _BotClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "testasd",
                    cancellationToken: cancellationToken);
            }

            CustomLogger<UpdateHandlerServiceImplementation>.Write(message.Text, ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            CustomLogger<UpdateHandlerServiceImplementation>.Write(ex.Message, ConsoleColor.Red);
            throw new Exception(ex.Message, ex);
        }
    }

    private async Task OnCallbackQueryData(CallbackQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var response = JsonSerializer.Deserialize<LanguageChoiseResponse>(query.Data!, _jsonSerializerOptions);
            if (response is null)
                return;

            var chatId = query.Message!.Chat.Id;
            //var user = await

            if (response.Direction == KeyboardDirection.Source) 
            {

            }
            else
            {

            }


            var language = await _languageManager.GetLanguageByCodeAsync(response.Code);
            string text = $"{response.Direction} language successfully changed to {language}";

            await _BotClient.EditMessageTextAsync(
                chatId: chatId,
                messageId: response.MessageId,
                text: text,
                cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            CustomLogger<UpdateHandlerService>.Write(ex.Message, ConsoleColor.Red);
            throw new Exception(ex.Message, ex);

            // HandleErrorAsync(....;
        }
    }
}
