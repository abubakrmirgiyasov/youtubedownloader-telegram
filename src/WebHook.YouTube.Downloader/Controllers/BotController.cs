using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WebHook.YouTube.Downloader.Services;

namespace WebHook.YouTube.Downloader.Controllers;

[ApiController]
[Route("[controller]")]
public class BotController : ControllerBase
{
    private readonly ILogger<BotController> _logger;

    public BotController(ILogger<BotController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        [FromServices] UpdateHandlerServiceBase updateHandler,
        CancellationToken cancellationToken)
    {
        try
        {
            updateHandler.MessageReceived += OnMessageReceived;
            updateHandler.ErrorOccurred += OnErrorOccurred;
            updateHandler.UnknownUpdateTypeReceived += OnUnknownUpdateTypeReceived;

            await updateHandler.HandleUpdateAsync(update, cancellationToken);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    private Task OnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[POST] received an update message: {MessageText}", message.Text);
        return Task.CompletedTask;
    }
 
    private Task OnErrorOccurred(Exception exception, CancellationToken cancelToken)
    {
        _logger.LogError("[POST] An error occurred during processing update: {ErrorMessage}", exception.Message);
        return Task.CompletedTask;
    }

    private Task OnUnknownUpdateTypeReceived(Update update, CancellationToken cancelToken)
    {
        _logger.LogWarning(
            "[POST] Retrieved update with unsupported type: {UpdateType} ({InternalUpdateType})", 
            update.Type,
            update.Message?.Type);
        return Task.CompletedTask;
    }
}
