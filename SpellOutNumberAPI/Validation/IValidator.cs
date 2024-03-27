namespace SpellOutNumberAPI.Validation;

public interface IValidator
{
    bool HasValue(string? input);
    bool AnyNonNumericChar(string input);
}