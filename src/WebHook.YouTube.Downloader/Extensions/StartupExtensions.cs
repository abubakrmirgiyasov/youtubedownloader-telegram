using System.Text.Json;
using Telegram.Bot;
using WebHook.YouTube.Downloader.Services;
using WebHook.YouTube.Downloader.Services.Commands;

namespace WebHook.YouTube.Downloader.Extensions;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration);
        var botConfiguration = builder.Configuration.GetRequiredConfigurationInstance<TelegramSettings>("TelegramSettings");
        botConfiguration.Validate();

        builder.Services
            .AddHttpClient("YouTubeDownloader")
            .AddTypedClient<ITelegramBotClient>();

        builder.Services.AddSingleton(_ => new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
        });

        builder.Services.AddScoped<ITelegramBotClient>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var botClientOptions = new TelegramBotClientOptions(botConfiguration.BotToken);
            return new TelegramBotClient(
                options: botClientOptions,
                httpClient: httpClientFactory.CreateClient("YouTubeDownloader"));
        });

        builder.Services.AddScoped<UpdateHandlerServiceBase, UpdateHandlerServiceImplementation>();

        builder.Services.AddCommandManager((serviceProvider, commandManagerBuilder) =>
        {
            //const int ReplyKeyboardColumns = 3;

            var serializerOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();

            commandManagerBuilder.RegisterCommand(new StartCommand());
        });

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHostedService<WebHookBackgroundService>();

        return builder;
    }

    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
