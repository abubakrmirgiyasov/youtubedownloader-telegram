using WebHook.YouTube.Downloader.Services.Commands;

namespace WebHook.YouTube.Downloader.Services.Builders;

public class CommandManagerBuilder
{
    private readonly IList<ICommand> _commands;
    private readonly IServiceScope _scope;

    public CommandManagerBuilder(IServiceScope scope)
    {
        _commands = new List<ICommand>();
        _scope = scope;
    }

    public CommandManagerBuilder RegisterCommand(ICommand command)
    {
        _commands.Add(command);
        return this;
    }

    public CommandManager Build()
    {
        return new CommandManager(_commands, _scope);
    }
}
