using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SpellOutNumberAPI.Business.Culture;
using SpellOutNumberAPI.Business.Spelling;
using SpellOutNumberAPI.Validation;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .AddScoped<IValidator, Validator>()
            .AddScoped<ILocalizationService, LocalizationService>()
            .AddScoped<ISpellerProvider, SpellerProvider>();
    })
    .Build();

host.Run();