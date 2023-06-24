using WebHook.YouTube.Downloader.Services.Builders;

namespace WebHook.YouTube.Downloader.Extensions;

public static class ServiceCollectionExtensions
{
    public delegate void ConfigureCommands(IServiceProvider serviceProvider, CommandManagerBuilder builder);

    public static IServiceCollection AddCommandManager(this IServiceCollection services, ConfigureCommands configureCommands)
    {
        services.AddScoped(serviceProvider =>
        {
            var scope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var builder = new CommandManagerBuilder(scope);
            configureCommands(scope.ServiceProvider, builder);
            return builder.Build();
        });
        return services;
    }
}
