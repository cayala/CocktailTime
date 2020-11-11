using Azure.Identity;
using CocktailTimeFunctions.HttpClients;
using CosmosDB;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsService;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(CocktailTimeFunctions.Startup))]
namespace CocktailTimeFunctions
{
    public class Startup : FunctionsStartup
    {
        public Startup() {}

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddAzureAppConfiguration(options =>
            {
                options.Connect(Environment.GetEnvironmentVariable("AzureAppConfig")).ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });
            });
            var Configuration = configBuilder.Build();

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
