using System.Globalization;
using SpellOutNumberAPI.Repo;

namespace SpellOutNumberAPI.Business.Culture;

public interface ILocalizationService
{
    public CultureInfo GetCulture(string input);
    public ISpellRepo GetLocalizedData(CultureInfo cultureInfo);
}