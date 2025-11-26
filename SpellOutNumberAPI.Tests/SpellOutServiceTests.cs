using SpellOutNumberAPI.Business;
using SpellOutNumberAPI.Business.Culture;
using SpellOutNumberAPI.Business.Spelling;

namespace SpellOutNumberAPI.Tests;

[TestFixture]
public class SpellOutServiceTests
{
    private ISpellOutService _service;

    [SetUp]
    public void Setup()
    {
        var localizationService = new LocalizationService();
        var spellerFactory = new SpellerFactory(localizationService);
        _service = new SpellOutService(spellerFactory);
    }

    [TestCase(0, "Zero")]
    [TestCase(13, "Thirteen")]
    [TestCase(25, "Twenty five")]
    [TestCase(100, "One hundred")]
    [TestCase(5555, "Five thousand five hundred fifty five")]
    public void SpellOut_GivenNumber_ReturnsSpelledOutValue(int number, string expected)
    {
        //Act
        var result = _service.SpellOut(number);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0, "en-US", "Zero")]
    [TestCase(13, "en-US", "Thirteen")]
    [TestCase(100, "english", "One hundred")]
    public void SpellOut_GivenNumberAndEnglishCulture_ReturnsEnglishSpelling(int number, string culture, string expected)
    {
        //Act
        var result = _service.SpellOut(number, culture);

        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SpellOut_GivenNullCulture_UsesEnglishAsDefault()
    {
        //Act
        string culture = null!;
        var result = _service.SpellOut(42, culture);

        //Assert
        Assert.That(result, Is.EqualTo("Forty two"));
    }

    [Test]
    public void SpellOut_GivenNegativeNumber_ThrowsArgumentException()
    {
        //Act
        var action = () => _service.SpellOut(-1);

        //Assert
        Assert.That(action, Throws.ArgumentException.With.Message.Contains("Number must be positive"));
    }

    [TestCase("es")]
    [TestCase("es-ES")]
    [TestCase("fr")]
    public void SpellOut_GivenUnsupportedCulture_ThrowsLocalizationServiceArgumentException(string culture)
    {
        //Act
        var action = () => _service.SpellOut(42, culture);

        //Assert
        Assert.That(action, Throws.InstanceOf<LocalizationServiceArgumentException>());
    }

    [TestCase(0, null)]
    [TestCase(0, "")]
    [TestCase(0, "   ")]
    public void SpellOut_GivenEmptyOrWhitespaceCulture_UsesEnglishAsDefault(int number, string? culture)
    {
        //Act
        var result = _service.SpellOut(number, culture);

        //Assert
        Assert.That(result, Is.EqualTo("Zero"));
    }
}