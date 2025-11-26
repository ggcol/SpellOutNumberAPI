namespace SpellOutNumberAPI.Business.Spelling;

public interface ISpellerFactory
{
    public ISpeller Get(string inputCulture);
}