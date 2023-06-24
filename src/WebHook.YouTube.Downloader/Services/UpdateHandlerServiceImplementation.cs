using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace WebHook.YouTube.Downloader.Services;

public class UpdateHandlerServiceImplementation : UpdateHandlerServiceBase
{
	private readonly CommandManager _command;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public UpdateHandlerServiceImplementation(
		ITelegramBotClient botClient, 
		CommandManager commandManager, 
		JsonSerializerOptions jsonSerializerOptions) : base(botClient)
	{
		_command = commandManager;
		_jsonSerializerOptions = jsonSerializerOptions;

        MessageReceived += OnMessageReceived;
	}

    private async Task OnMessageReceived(Message message, CancellationToken cancellationToken)
    {
		try
		{
            var chatId = message.Chat.Id;
			var ytdl = new YoutubeDL();
            if (message.Text!.StartsWith('/'))
            {
				await _command.HandleCommandAsync(
					chatId: chatId,
					command: message.Text,
					botClient: BotClient,
					cancellationToken: cancellationToken);
			}
			else
			{
				var options = new OptionSet()
				{
					GetUrl = true,
				};
				var link = ytdl.RunWithOptions(new[] { message.Text }, options, cancellationToken);
				//var video = link.Data;
				//var title = video.Title;
				//var uploader = video.Uploader;
				//var views = video.ViewCount;
				//var formats = video.Formats;

				string text = "wait...";

				await BotClient.SendTextMessageAsync(
					chatId: chatId,
					text: text,
					cancellationToken: cancellationToken);
			}
        }
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
    }
}
