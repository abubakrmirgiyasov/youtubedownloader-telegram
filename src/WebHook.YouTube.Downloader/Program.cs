using WebHook.YouTube.Downloader.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices().Build();

app.Configure();
app.Run();
