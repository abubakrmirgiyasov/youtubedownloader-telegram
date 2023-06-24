namespace WebHook.YouTube.Downloader.Extensions;

public static class MulticastDelegateExtensions
{
    public static IEnumerable<Task>? InvokeAll<T>(this T multicastDelegate, Func<T, Task> invocationFunctions)
        where T : MulticastDelegate
    {
        return multicastDelegate?
            .GetInvocationList()
            .Cast<T>()
            .Select(invocationFunctions.Invoke)
            .ToArray();
    }
}
