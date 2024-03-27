using SpellOutNumberAPI.Validation;

namespace SpellOutNumberAPI.Tests;

[TestFixture]
public class ValidatorTests
{
    private IValidator _validate = new Validator();

    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void HasValue_GivenAEmptyOrNullString_ReturnFalse(string input)
    {
        //Act
        var hasValue = _validate.HasValue(input);

        //Assert
        Assert.That(hasValue, Is.False);
    }

    [TestCase("42")]
    [TestCase("any")]
    [TestCase("Hello, world!")]
    public void HasValue_GivenAString_ReturnTrue(string input)
    {
        //Act
        var hasValue = _validate.HasValue(input);
        
        //Assert
        Assert.That(hasValue, Is.True);
    }

    [TestCase("4any2")]
    [TestCase("any42")]
    [TestCase("42any")]
    public void AnyNonNumericChar_GivenAStringWithNonNumericChar_ReturnTrue(
        string input)
    {
        //Act
        var any = _validate.AnyNonNumericChar(input);

        //Assert
        Assert.That(any, Is.True);
    }

    [TestCase("42")]
    [TestCase("2147483648")] //int.MaxValue+1
    public void AnyNonNumericChar_GivenAStringWithOnlyNumericChar_ReturnFalse(
        string input)
    {
        //Act
        var any = _validate.AnyNonNumericChar(input);

        //Assert
        Assert.That(any, Is.False);
    }
}