using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpellOutNumberAPI.Business;
using SpellOutNumberAPI.Business.Culture;
using SpellOutNumberAPI.Business.Spelling;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddScoped<ILocalizationService, LocalizationService>()
    .AddScoped<ISpellerFactory, SpellerFactory>()
    .AddScoped<ISpellOutService, SpellOutService>();

builder.Build().Run();