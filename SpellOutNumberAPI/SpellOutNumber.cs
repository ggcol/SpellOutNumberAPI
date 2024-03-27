using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SpellOutNumberAPI.Validation;

namespace SpellOutNumberAPI;

public class SpellOutNumber
{
    private readonly ISpeller _speller;
    private readonly IValidator _validate;

    public SpellOutNumber(
        ILogger<SpellOutNumber> logger,
        ISpeller speller,
        IValidator validate)
    {
        _speller = speller;
        _validate = validate;
    }

    [Function("SpellOutNumber")]
    [HttpGet]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestData req)
    {
        var input = req.Query["number"];

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

        var spelledOut = _speller.SpellOut(number);

        return new OkObjectResult(spelledOut);
    }
}