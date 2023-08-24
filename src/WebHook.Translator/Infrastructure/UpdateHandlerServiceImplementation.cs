using System.Text.Json;
using System.Threading;
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
            var response = JsonSerializer.Deserialize<ChoiceResponse>(query.Data!, _jsonSerializerOptions);
            if (response is null)
                return;

            long chatId = query.Message!.Chat.Id;
            
            switch (response.MarkupType)
            {
                case MarkupType.Language:
                    await LanguageEdit(chatId, response, cancellationToken);
                    break;
                case MarkupType.Play:
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            CustomLogger<UpdateHandlerService>.Write(ex.Message, ConsoleColor.Red);
            throw new Exception(ex.Message, ex);

            // HandleErrorAsync(....;
        }
    }

    private async Task LanguageEdit(long chatId, ChoiceResponse response, CancellationToken cancellationToken)
    {
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
}
