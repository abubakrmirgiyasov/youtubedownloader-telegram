using System.Text.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;
using YoutubeDLSharp.Options;
using static System.Net.Mime.MediaTypeNames;

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
			var ytdl = new YoutubeDL() { YoutubeDLPath = "yt-dlp.exe" };
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
				string text = "Loading...";

                var loading = await BotClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: text,
                    cancellationToken: cancellationToken);

                var link = await ytdl.RunVideoDataFetchAsync(message.Text);
				var video = link.Data;
				var title = video.Title;
				var uploader = video.Uploader;
				var views = video.ViewCount;
				var formats = video.Formats.Where(x => x.Extension == "webm");

				//await BotClient.DeleteMessageAsync(
				//	chatId: chatId,
				//	messageId: loading.MessageId,
				//	cancellationToken: cancellationToken);

                //await BotClient.DeleteMessageAsync(
                //    chatId: chatId,
                //    messageId: message.MessageId,
                //    cancellationToken: cancellationToken);

                await BotClient.SendPhotoAsync(
					chatId: chatId,
					photo: InputFile.FromUri(link.Data.Thumbnail),
					caption: GenerateCaption(link.Data.Title, message.Text, link.Data.UploaderUrl, link.Data.Uploader),
                    parseMode: ParseMode.Html,
                    replyMarkup: null,
					disableNotification: false,
					cancellationToken: cancellationToken);
			}
        }
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
    }

	private FormatData[] GetFormats(IEnumerable<FormatData> formats)
	{
		var data = new List<FormatData>();

		foreach (var format in formats)
		{
			if (format.Resolution.IndexOf("audio only") != -1)
			{
				data.Add(format);
			}
			else if (format.Height == 360 || format.Height == 720 || format.Height == 1080)
			{
				data.Add(format);
			}
		}
		return data.ToArray();
	}

	private string GenerateCaption(string title, string videoLink, string channelLink, string channelName)
	{
		return
			$"<a href=\"{videoLink}\"> 📹 {title}</a>" +
			$"{Environment.NewLine}" +
			$"<a href=\"{channelLink}\"> 👤 {channelName}</a>" +
			$"<code >🚀 {"SADASD"}</code>";
	}
	//private async Task Loader(bool isStop, long chatId, )
	//{
	//	await Task.Yield();

	//	while (isStop)
	//	{
 //           await BotClient.SendTextMessageAsync(
 //           chatId: chatId,
 //           text: "wait",
 //           cancellationToken: cancellationToken);
 //       }

        
 //   }
}
