namespace WebHook.YouTube.Downloader.Services;

public abstract class ConfigurationBase
{
    public abstract void Validate();

    protected void ValidateNotNull<T>(string sectionPath, string propertyName, T propertyValue)
    {
        bool isValid;

        if (propertyValue is string stringPropertyValue)
            isValid = !string.IsNullOrWhiteSpace(stringPropertyValue);
        else
            isValid = propertyValue is not null;

        if (!isValid)
            throw new InvalidOperationException(
                $"'[{sectionPath}:{propertyName}]' configuration value is not provided.");
    }
}
