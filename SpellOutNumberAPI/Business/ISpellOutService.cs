namespace SpellOutNumberAPI.Business;

public interface ISpellOutService
{
    public string SpellOut(int number, string? culture = null);
}