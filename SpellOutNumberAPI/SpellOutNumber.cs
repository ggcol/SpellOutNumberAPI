using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SpellOutNumberAPI.Business.Culture;
using SpellOutNumberAPI.Business.Spelling;
using SpellOutNumberAPI.Validation;

namespace SpellOutNumberAPI;

public class SpellOutNumber
{
    private readonly IValidator _validate;
    private readonly ISpellerProvider _spellerProvider;

    public SpellOutNumber(
        ILogger<SpellOutNumber> logger,
        IValidator validate,
        ISpellerProvider spellerProvider)
    {
        _validate = validate;
        _spellerProvider = spellerProvider;
    }

    [Function(nameof(SpellOut))]
    [Route("/{input}/{culture}")]
    [HttpGet]
    public IActionResult SpellOut(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestData req, string input, string? culture)
    {
        if (!_validate.HasValue(input))
        {
            return new BadRequestResult();
        }

        if (_validate.AnyNonNumericChar(input!))
        {
            return new BadRequestObjectResult("Not a number, unable to parse!");
        }

        var isParsed = int.TryParse(input, out var number);

        if (!isParsed)
        {
            return new BadRequestObjectResult(
                $"Number out of range, must be positive and less than {int.MaxValue}");
        }

        if (number < 0)
        {
            return new BadRequestObjectResult("Number must be positive!");
        }

        try
        {
            var spelledOut = _spellerProvider
                .GetSpeller(
                    _validate.HasValue(culture)
                        ? culture!
                        : KnownCultures.English)
                .SpellOut(number);

            return new OkObjectResult(spelledOut);
        }
        catch (LocalizationServiceArgumentException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}