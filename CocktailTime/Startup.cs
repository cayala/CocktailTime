using CocktailTime.Repositories;
using CocktailTime.Repositories.Interfaces;
using CosmosDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CocktailTime
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //research proper way to istantiate service via cosmos since there is a dependency
            //Note: The configuration for these values are hidden away from view in the .Net Core Manage App Secrets which can be accessed by right clicking the project and hitting "Manage Secrets"
            //Note: These secrets are stored on disk in the AppData folder and these values are only retrieved if the application Environment variable is set to "Development"
            //Note: Link for above notes -> https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows
            var cosmosService = new CosmosService(Configuration["Cosmos:database:name"], Configuration["Cosmos:database:containers:recipients"], new CosmosClient(Configuration["Cosmos:account"], Configuration["Cosmos:key"]));
            services.AddSingleton<ICosmosService>(cosmosService);
            services.AddSingleton<ICocktailRepository>(new CocktailRepository(cosmosService));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktail Time");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
