using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SpellOutNumberAPI;
using SpellOutNumberAPI.Repo;
using SpellOutNumberAPI.Validation;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<ISpellRepo, SpellRepo>();
        services.AddScoped<ISpeller, Speller>();
        services.AddScoped<IValidator, Validator>();
    })
    .Build();

host.Run();