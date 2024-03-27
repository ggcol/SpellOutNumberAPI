namespace SpellOutNumberAPI.Repo;

public class ItSpellRepo : ISpellRepo
{
    public string Zero => "Zero";
    public string Hundred => "Cento";

    public IReadOnlyList<string> Units => new[]
    {
        "", "Uno", "Due", "Tre", "Quattro", "Cinque", "Sei", "Sette", "Otto",
        "Nove"
    };

    public IReadOnlyList<string> Teens => new[]
    {
        "Dieci", "Undici", "Dodici", "Tredici", "Quattordici", "Quindici",
        "Sedici", "Diciassette", "Diciotto", "Diciannove"
    };

    public IReadOnlyList<string> Tens => new[]
    {
        "", "", "Venti", "Trenta", "Quaranta", "Cinquanta", "Sessanta",
        "Settanta", "Ottanta", "Novanta"
    };

    public IReadOnlyList<string> Thousands => new[]
        { "", "Mille", "Millioni", "Miliardi", "Triliardi" };
}