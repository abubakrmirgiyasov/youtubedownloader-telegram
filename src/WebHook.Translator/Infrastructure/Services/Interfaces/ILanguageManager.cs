using WebHook.Translator.Models;

namespace WebHook.Translator.Infrastructure.Services.Interfaces;

public interface ILanguageManager
{
    Task<IEnumerable<Language>> GetLanguagesAsync();

    Task<Language> GetLanguageByCodeAsync(string code);
}
