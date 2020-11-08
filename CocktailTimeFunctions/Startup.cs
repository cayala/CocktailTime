using CosmosDB;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmsService;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.IO;
using CocktailTimeFunctions.HttpClients;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Azure;

[assembly: FunctionsStartup(typeof(CocktailTimeFunctions.Startup))]
namespace CocktailTimeFunctions
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ISMSClient>(new SMSClient(Configuration["CommunicationService"]));
            builder.Services.AddSingleton<ICosmosService>(new CosmosService(Configuration["Cosmos:database:name"], Configuration["Cosmos:database:containers:recipients"], new CosmosClient(Configuration["Cosmos:account"], Configuration["Cosmos:key"])));
            builder.Services.AddSingleton<CocktailDBHttpClient>(new CocktailDBHttpClient(new Uri(Configuration["ExternalWebApis:CocktailDB"])));
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            base.ConfigureAppConfiguration(builder);
        }
    }
}
