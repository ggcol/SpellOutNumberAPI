using System.Globalization;
using SpellOutNumberAPI.Repo;

namespace SpellOutNumberAPI.Business.Culture;

public class LocalizationService : ILocalizationService
{
    public CultureInfo GetCulture(string input)
    {
        return input.ToLower() switch
        {
            "en" => new CultureInfo(KnownCultures.English),
            "en-gb" => new CultureInfo(KnownCultures.English),
            "en-uk" => new CultureInfo(KnownCultures.English),
            "en-us" => new CultureInfo(KnownCultures.English),
            "english" => new CultureInfo(KnownCultures.English),
            "it" => new CultureInfo(KnownCultures.Italian),
            "it-it" => new CultureInfo(KnownCultures.Italian),
            "italian" => new CultureInfo(KnownCultures.Italian),
            "italiano" => new CultureInfo(KnownCultures.Italian),
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