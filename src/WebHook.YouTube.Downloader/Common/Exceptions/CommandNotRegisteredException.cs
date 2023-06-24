namespace WebHook.YouTube.Downloader.Common.Exceptions;

public class CommandNotRegisteredException : ApplicationException
{
    public CommandNotRegisteredException(string commandName)
        : base($"Command '[{commandName}]' not registered.") { }
}
