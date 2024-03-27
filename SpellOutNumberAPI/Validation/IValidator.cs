namespace SpellOutNumberAPI.Validation;

public interface IValidator
{
    public bool HasValue(string? input);
    public bool AnyNonNumericChar(string input);
}