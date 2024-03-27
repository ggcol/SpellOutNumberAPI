using NuGet.Frameworks;
using SpellOutNumberAPI.Business.Culture;

namespace SpellOutNumberAPI.Tests;

[TestFixture]
public class LocalizationServiceTests
{
    private ILocalizationService _localization => new LocalizationService();

    [TestCase("en")]
    [TestCase("en-GB")]
    [TestCase("en-UK")]
    [TestCase("en-US")]
    [TestCase("english")]
    public void Get_GivenEnglish_ReturnEnglishCultureInfo(string input)
    {
        //Act
        var cultureInfo = _localization.GetCulture(input);

        //Assert
        Assert.That(cultureInfo.Name, Is.EqualTo(KnownCultures.English));
    }

    [TestCase("it")]
    [TestCase("it-IT")]
    [TestCase("italian")]
    [TestCase("italiano")]
    public void Get_GivenItalian_ReturnItalianCultureInfo(string input)
    {
        //Act
        var cultureInfo = _localization.GetCulture(input);

        //Assert
        Assert.That(cultureInfo.Name, Is.EqualTo(KnownCultures.Italian));
    }

    [TestCase("it", "IT")]
    [TestCase("english", "ENGLIsh")]
    public void Get_GivenAString_IsCaseInsensitive(string input1, string input2)
    {
        //Act
        var cultureInfo1 = _localization.GetCulture(input1);
        var cultureInfo2 = _localization.GetCulture(input2);
        
        //Assert
        Assert.That(cultureInfo1, Is.EqualTo(cultureInfo2));
    }

    [TestCase("es")]
    [TestCase("es-ES")]
    public void Get_GivenUnsupportedCulture_ThrowsException(string input)
    {
        //Act
        var cultureInfo = () => _localization.GetCulture(input);

        //Assert
        Assert.That(cultureInfo, Throws.Exception);
    }
}