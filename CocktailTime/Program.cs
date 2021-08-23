using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureAppConfiguration((ctx, config) =>
                {
                    var settings = config.Build();
                    //config.AddAzureAppConfiguration(options =>
                    //{
                    //    options.Connect(settings["AppConfig:connectionString"])
                    //    .ConfigureKeyVault(kv =>
                    //    {
                    //        //Be sure to set system environment variables and restart your machine that way they take affect.
                    //        kv.SetCredential(new DefaultAzureCredential());
                    //    });
                    //});
                }).UseStartup<Startup>();
            });
    }
}
