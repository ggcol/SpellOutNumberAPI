using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SpellOutNumberAPI.Business.Culture;
using SpellOutNumberAPI.Business.Spelling;

namespace SpellOutNumberAPI;

public sealed class SpellOut(ISpellerFactory spellerFactory)
{
    [Function("SpellOut_v2")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "spellout/{input:int}")]
        HttpRequestData req, 
        int input,
        FunctionContext executionContext)
    {
        var rx = req.CreateResponse(HttpStatusCode.BadRequest);
        rx.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        if (input < 0)
        {
            await rx.WriteStringAsync("Number must be positive!").ConfigureAwait(false);
            return rx;
        }

        try
        {
            var culture = req.Query["culture"];

            var spelledOut = spellerFactory
                .Get(
                    string.IsNullOrWhiteSpace(culture)
                        ? KnownCultures.English
                        : culture)
                .SpellOut(input);

            rx = req.CreateResponse(HttpStatusCode.OK);
            await rx.WriteStringAsync(spelledOut).ConfigureAwait(false);
            return rx;
        }
        catch (LocalizationServiceArgumentException ex)
        {
            await rx.WriteStringAsync(ex.Message).ConfigureAwait(false);
            return rx;
        }
    }
}