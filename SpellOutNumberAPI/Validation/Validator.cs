using System.Text.RegularExpressions;

namespace SpellOutNumberAPI.Validation;

public class Validator : IValidator
{
    public bool HasValue(string? input)
    {
        return !string.IsNullOrWhiteSpace(input);
    }

    public bool AnyNonNumericChar(string input)
    {
        const string onlyNumber = @"^\d+$";
        return !Regex.IsMatch(input, onlyNumber);
    }
}