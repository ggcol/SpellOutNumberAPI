using System.Globalization;
using SpellOutNumberAPI.Repo;

namespace SpellOutNumberAPI.Business.Culture;

internal sealed class LocalizationService : ILocalizationService
{
    public CultureInfo GetCulture(string input)
    {
        return input.ToLower() switch
        {
            "en" or "en-gb" or "en-uk" or "en-us" or "english" => new CultureInfo(KnownCultures.English),
            "it" or "it-it" or "italian" or "italiano" => new CultureInfo(KnownCultures.Italian),
            _ => throw new LocalizationServiceArgumentException(
                "Not implemented culture")
        };
    }

    public ISpellRepo GetLocalizedData(CultureInfo cultureInfo)
    {
        return cultureInfo.Name switch
        {
            KnownCultures.Italian => new ItSpellRepo(),
            KnownCultures.English => new EnSpellRepo(),
            _ => throw new LocalizationServiceArgumentException(
                "Not implemented culture")
        };
    }
}