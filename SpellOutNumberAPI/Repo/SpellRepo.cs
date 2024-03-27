namespace SpellOutNumberAPI.Repo;

public class SpellRepo : ISpellRepo
{
    public string Zero => "Zero";
    public string Hundred => "Hundred";

    public IReadOnlyList<string> Units => new[]
    {
        "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
        "Nine"
    };

    public IReadOnlyList<string> Teens => new[]
    {
        "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
        "Sixteen", "Seventeen", "Eighteen", "Nineteen"
    };

    public IReadOnlyList<string> Tens => new[]
    {
        "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy",
        "Eighty", "Ninety"
    };

    public IReadOnlyList<string> Thousands => new[]
        { "", "Thousand", "Million", "Billion", "Trillion" };
}