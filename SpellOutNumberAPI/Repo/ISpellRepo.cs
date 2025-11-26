namespace SpellOutNumberAPI.Repo;

internal interface ISpellRepo
{
    public string Zero { get; }
    public string Hundred { get; }
    public IReadOnlyList<string> Units { get; }
    public IReadOnlyList<string> Teens { get; }
    public IReadOnlyList<string> Tens { get; }
    public IReadOnlyList<string> Thousands { get; }
}