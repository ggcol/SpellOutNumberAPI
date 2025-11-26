using SpellOutNumberAPI.Business.Culture;

namespace SpellOutNumberAPI.Business.Spelling;

internal sealed class SpellerFactory(ILocalizationService localization)
    : ISpellerFactory
{
    public ISpeller Get(string inputCulture)
    {
        var culture = localization.GetCulture(inputCulture);
        var repo = localization.GetLocalizedData(culture);
        return new Speller(repo);
    }
}