using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

namespace CocktailTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //This is added in to have Azure pull secrets and api keys from keyvault
            //.ConfigureAppConfiguration((ctx, builder) =>
            //{
            //    var keyVaultEndpoint = GetKeyVaultEndpoint();
                
            //    if (!string.IsNullOrEmpty(keyVaultEndpoint))
            //        builder.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential(), new KeyVaultSecretManager());
            //})
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        static string GetKeyVaultEndpoint()
            => Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT");
    }
}
