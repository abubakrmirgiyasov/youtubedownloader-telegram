﻿namespace WebHook.YouTube.Downloader.Extensions;

public static class ConfigurationManagerExtensions
{
    public static T GetRequiredConfigurationInstance<T>(this ConfigurationManager configurationManager, string sectionPath)
        where T : new()
    {
        var configurationSection = configurationManager.GetSection(sectionPath);
        var configurationInstance = new T();
        configurationSection.Bind(configurationInstance);
        return configurationInstance;
    }
}
