using SpellOutNumberAPI.Business.Culture;
using SpellOutNumberAPI.Business.Spelling;

namespace SpellOutNumberAPI.Business;

internal sealed class SpellOutService(ISpellerFactory spellerFactory) : ISpellOutService
{
    public string SpellOut(int number, string? culture = null)
    {
        if (number < 0)
        {
            throw new ArgumentException("Number must be positive!", nameof(number));
        }

        var effectiveCulture = string.IsNullOrWhiteSpace(culture) 
            ? KnownCultures.English 
            : culture;

        var speller = spellerFactory.Get(effectiveCulture);
        return speller.SpellOut(number);
    }
}