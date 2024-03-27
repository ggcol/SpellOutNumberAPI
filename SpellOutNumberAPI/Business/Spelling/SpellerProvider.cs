using SpellOutNumberAPI.Business.Culture;

namespace SpellOutNumberAPI.Business.Spelling;

public interface ISpellerProvider
{
    ISpeller GetSpeller(string inputCulture);
}

public class SpellerProvider(ILocalizationService localization)
    : ISpellerProvider
{
    public ISpeller GetSpeller(string inputCulture)
    {
        var culture = localization.GetCulture(inputCulture);
        var repo = localization.GetLocalizedData(culture);
        return new Speller(repo);
    }
}