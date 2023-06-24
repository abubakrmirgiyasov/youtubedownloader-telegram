using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot;
using WebHook.YouTube.Downloader.Common.Exceptions;
using WebHook.YouTube.Downloader.Services.Commands;

namespace WebHook.YouTube.Downloader.Services;

public sealed partial class CommandManager : IDisposable
{
    [GeneratedRegex("\\s{2,}")]
    private static partial Regex GetAnyWhiteSpaceCharactersPattern();
    private bool _isDisposed;

    private readonly IEnumerable<ICommand> _commands;
    private readonly IServiceScope _service;

    public CommandManager(IEnumerable<ICommand> commands, IServiceScope service)
    {
        _commands = commands;
        _service = service;
    }

    ~CommandManager()
    {
        Dispose(false);
    }

    public async Task HandleCommandAsync(
        long chatId,
        string command,
        ITelegramBotClient botClient,
        CancellationToken cancellationToken)
    {
        try
        {
            string? arguments = default;
            string commandName;

            var splittedCommand = GetAnyWhiteSpaceCharactersPattern()
                .Replace(command.Trim(), " ")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            commandName = splittedCommand.First();

            if (splittedCommand.Length > 1)
                arguments = splittedCommand
                    .Skip(1)
                    .Aggregate(
                        seed: new StringBuilder(),
                        func: (resultBuilder, argument) => resultBuilder.Append($" {argument}"),
                        resultSelector: resultBuilder => resultBuilder.ToString());
            var registerCommand =
                _commands.FirstOrDefault(item => item.GetCommandName().Equals(commandName))
                ?? throw new CommandNotRegisteredException(commandName);

            await registerCommand.HandleCommand(
                chatId: chatId,
                botClient: botClient,
                cancellationToken: cancellationToken,
                arguments: arguments);
        }
        catch (CommandNotRegisteredException ex)
        {
            throw new CommandNotRegisteredException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private void Dispose(bool needToDispose)
    {
        if (_isDisposed)
        {
            if (needToDispose)
            {
                _service.Dispose();
            }
            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
