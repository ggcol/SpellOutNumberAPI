using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SpellOutNumberAPI.Business;

namespace SpellOutNumberAPI;

public sealed class SpellOut(ISpellOutService spellOutService)
{
    [Function(nameof(SpellOut))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "spellout/{input:int}")]
        HttpRequestData req,
        int input,
        FunctionContext executionContext)
    {
        var culture = req.Query["culture"];

        try
        {
            var spelledOut = spellOutService.SpellOut(input, culture);
            return await MakeResponse(req, HttpStatusCode.OK, spelledOut).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return await MakeResponse(req, HttpStatusCode.BadRequest, ex.Message).ConfigureAwait(false);
        }
    }

    private static async Task<HttpResponseData> MakeResponse(HttpRequestData req, HttpStatusCode statusCode, string body)
    {
        var res = req.CreateResponse(statusCode);
        res.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await res.WriteStringAsync(body).ConfigureAwait(false);
        return res;
    }
}