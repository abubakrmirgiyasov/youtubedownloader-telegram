using System.Text.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebHook.YouTube.Downloader.Common;
using WebHook.YouTube.Downloader.Extensions;
using WebHook.YouTube.Downloader.Utils;
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
			var ytdl = new YoutubeDL() { YoutubeDLPath = "bin\\Debug\\net7.0\\yt-dlp.exe" };
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
				
				if (!link.Success)
					await BotClient.SendTextMessageAsync(
						chatId: chatId,
						text: string.Join(Environment.NewLine, link.ErrorOutput),
						cancellationToken: cancellationToken);

				var formats = link.Data.Formats
					.Where(x => x.Extension == "webm")
					.DistinctBy(x => x.FormatNote);

				await BotClient.DeleteMessageAsync(
					chatId: chatId,
					messageId: loading.MessageId,
					cancellationToken: cancellationToken);

				await BotClient.DeleteMessageAsync(
					chatId: chatId,
					messageId: message.MessageId,
					cancellationToken: cancellationToken);

				var markups = TelegramUtilities.ParseDownloadCollectionKeyboardMarkup(3, message.MessageId, KeyboardDirection.Target, await new FormatManager().GetDownloadsFormatCollectionAsync(), _jsonSerializerOptions);

				await BotClient.SendPhotoAsync(
					chatId: chatId,
					photo: InputFile.FromUri(link.Data.Thumbnail),
					caption: GenerateCaption(link.Data.Title, message.Text, link.Data.UploaderUrl, link.Data.Uploader, GetFormats(formats)),
					parseMode: ParseMode.Html,
					replyMarkup: markups,
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
			if (format.FormatNote.IndexOf("medium") != -1)
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

	private string GenerateCaption(string title, string videoLink, string channelLink, string channelName, FormatData[] formats)
	{
        string formatAndSize = "";

		foreach (var format in formats)
		{
			formatAndSize += $"💾 " +
				$"{format.FormatNote} - " +
				$"{ConvertByteExtension.ToSize(format.FileSize, SizeUnits.MB)} MB " +
				$"{Environment.NewLine}" +
				$"{Environment.NewLine}";
		}

		return
			$"<a href=\"{videoLink}\">🎥 {title}</a>" +
			$"{Environment.NewLine}" +
			$"<a href=\"{channelLink}\">🗣 {channelName}</a>" +
            $"{Environment.NewLine}" +
			$"{Environment.NewLine}" +
			$"🚀 <b>Форматы:</b> " +
			$"{Environment.NewLine}" +
			$"{Environment.NewLine}" +
            $"<code>{formatAndSize}</code>";
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
