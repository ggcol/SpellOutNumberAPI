using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SpellOutNumberAPI;

public class SpellOutNumber(ILogger<SpellOutNumber> logger, ISpeller speller)
{
    [Function("SpellOutNumber")]
    [HttpGet]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestData req)
    {
        var stringNumber = req.Query["number"];
        
        if (string.IsNullOrWhiteSpace(stringNumber)) return new BadRequestResult();

        var isParsed = int.TryParse(stringNumber, out var number);

        if (!isParsed) return new BadRequestObjectResult("Not a number, unable to parse!");
        
        if (number < 0) return new BadRequestObjectResult("Number must be positive!");
        
        var spelledOut = speller.SpellOut(number);
        
        return new OkObjectResult(spelledOut);
    }
}