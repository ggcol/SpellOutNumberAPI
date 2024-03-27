using SpellOutNumberAPI.Business.Spelling;
using SpellOutNumberAPI.Repo;

namespace SpellOutNumberAPI.Tests;

[TestFixture]
public class SpellerTests
{
    [TestCase(0, "Zero")]
    [TestCase(13, "Thirteen")]
    [TestCase(25, "Twenty Five")]
    [TestCase(5555, "Five Thousand Five Hundred Fifty Five")]
    public void SpellOut_GivenANumber_ReturnsItsValueSpelled(int number, string expected)
    {
        //Arrange
        var repo = new EnSpellRepo();
        var speller = new Speller(repo);
        
        //Act
        var spelled = speller.SpellOut(number);
        
        //Assert
        Assert.That(spelled, Is.EqualTo(expected));
    }
}