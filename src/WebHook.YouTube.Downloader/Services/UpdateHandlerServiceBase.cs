using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebHook.YouTube.Downloader.Extensions;

namespace WebHook.YouTube.Downloader.Services;

public abstract partial class UpdateHandlerServiceBase
{
    public delegate Task MessageReceivedEventHandler(Message message, CancellationToken cancellationToken);
    public delegate Task MessageEditedEventHandler(Message message, CancellationToken cancellationToken);
    public delegate Task UnknownUpdateTypeReceivedEventHandler(Update update, CancellationToken cancelToken);
    public delegate Task CallBackQueryEventHandler(CallbackQuery query, CancellationToken cancelToken);
    public delegate Task ErrorOccurredEventHandler(Exception exception, CancellationToken cancelToken);

    public event MessageReceivedEventHandler? MessageReceived;
    public event UnknownUpdateTypeReceivedEventHandler? UnknownUpdateTypeReceived;
    public event MessageEditedEventHandler? MessageEdited;
    public event CallBackQueryEventHandler? CallBackQueryReceived;
    public event ErrorOccurredEventHandler? ErrorOccurred;
}

public abstract partial class UpdateHandlerServiceBase
{
    protected readonly ITelegramBotClient BotClient;

    protected UpdateHandlerServiceBase(ITelegramBotClient botClient)
    {
        BotClient = botClient;
    }

    public Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var handlers = update.Type switch
        {
            UpdateType.Message when update.Message!.Type == MessageType.Text => MessageReceived?.InvokeAll(handler => handler(update.Message!, cancellationToken)),
            UpdateType.CallbackQuery => CallBackQueryReceived?.InvokeAll(handler => handler(update.CallbackQuery!, cancellationToken)),
            UpdateType.EditedMessage => MessageEdited?.InvokeAll(handler => handler(update.Message!, cancellationToken)),
            _ => UnknownUpdateTypeReceived?.InvokeAll(handler => handler(update, cancellationToken))
        };

        return handlers is not null
            ? Task.WhenAll(handlers) 
            : Task.CompletedTask;
    }

    public Task HandleErrorAsync(Exception exception, CancellationToken cancelToken)
    {
        return
            ErrorOccurred?.Invoke(exception, cancelToken)
            ?? Task.CompletedTask;
    }
}