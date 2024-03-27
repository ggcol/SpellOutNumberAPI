using System.Globalization;
using SpellOutNumberAPI.Repo;

namespace SpellOutNumberAPI.Business.Spelling;

public class Speller(ISpellRepo spellRepo) : ISpeller
{
    public string SpellOut(int number)
    {
        if (number == 0)
        {
            return spellRepo.Zero;
        }
                
        var groupIndex = 0;
        var spelledOut = "";

        do
        {
            var group = number % 1000;
            if (group != 0)
            {
                var groupText = SpellOutGroup(group);
                spelledOut =
                    $"{groupText} {spellRepo.Thousands[groupIndex]} {spelledOut}";
            }

            number /= 1000;
            groupIndex++;
        } while (number > 0);

        return spelledOut.Trim();
    }

    private string SpellOutGroup(int group)
    {
        var spelledOutGroup = string.Empty;

        var hundreds = group / 100;
        if (hundreds > 0)
        {
            spelledOutGroup += $"{spellRepo.Units[hundreds]} {spellRepo.Hundred} ";
        }

        var lastTwoDigits = group % 100;

        switch (lastTwoDigits)
        {
            case < 10:
                spelledOutGroup += spellRepo.Units[lastTwoDigits];
                break;
            case < 20:
                spelledOutGroup += spellRepo.Teens[lastTwoDigits - 10];
                break;
            default:
            {
                var tensDigit = lastTwoDigits / 10;
                var unitsDigit = lastTwoDigits % 10;
                spelledOutGroup +=
                    $"{spellRepo.Tens[tensDigit]} {spellRepo.Units[unitsDigit]}";
                break;
            }
        }

        return spelledOutGroup.Trim();
    }
}