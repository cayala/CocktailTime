using CocktailTimeFunctions.HttpClients;
using CocktailTimeFunctions.Models;
using CosmosDB.Documents;
using CosmosDB.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailTimeFunctions
{
    public class Send
    {
        private readonly ISMSClient _SMS;
        private readonly ICosmosRead _RecipientsCosmosService;
        private readonly ICosmosCRUD _CocktailCacheCosmosService;
        private readonly CocktailDBHttpClient _HttpClient;

        public Send(ISMSClient sms, ICosmosRead recipientsCosmosService, ICosmosCRUD cocktailCacheCosmosService, CocktailDBHttpClient httpClient)
            => (_SMS, _RecipientsCosmosService, _CocktailCacheCosmosService, _HttpClient) = (sms, recipientsCosmosService, cocktailCacheCosmosService, httpClient);

        [FunctionName("Send")]
        //TODO: Need to alter this so it sends an email at 5 PM west coast, this is currently set to UTC time
        //public async Task Run([TimerTrigger("0 0 17 * * *")]TimerInfo myTimer, ILogger log)
        public async Task Run([TimerTrigger("10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            CocktailMessage cocktailMsg = null;

            //Every night at midnight utc, cache a brand new cocktail for the day
            if (DateTime.UtcNow.Hour == 0)
            {
                cocktailMsg = await _HttpClient.GetCocktailMessage();
                var result = await _RecipientsCosmosService.GetDocuments<CocktailMessageDocument>(new QueryDefinition(Constants.Cosmos.Query.CocktailTime.CocktailCache.GetCachedCocktail));
                var cachedCocktail = result.FirstOrDefault();

                if (cachedCocktail is null)
                    await _CocktailCacheCosmosService.AddDocument(new CocktailMessageDocument(cocktailMsg.Name, cocktailMsg.ServingGlass, cocktailMsg.Instructions, cocktailMsg.Image.ToString(), cocktailMsg.Ingredients));
                else
                {
                    await _CocktailCacheCosmosService.DeleteDocument<CocktailMessageDocument>(cachedCocktail.ID);
                    await _CocktailCacheCosmosService.AddDocument(new CocktailMessageDocument(cocktailMsg.Name, cocktailMsg.ServingGlass, cocktailMsg.Instructions, cocktailMsg.Image.ToString(), cocktailMsg.Ingredients));
                }
            }
            else
            {
                var result = await _RecipientsCosmosService.GetDocuments<CocktailMessageDocument>(new QueryDefinition(Constants.Cosmos.Query.CocktailTime.CocktailCache.GetCachedCocktail));
                var cachedCocktail = result.FirstOrDefault();

                if (cachedCocktail is null)
                {
                    cocktailMsg = await _HttpClient.GetCocktailMessage();
                    await _CocktailCacheCosmosService.AddDocument(new CocktailMessageDocument(cocktailMsg.Name, cocktailMsg.ServingGlass, cocktailMsg.Instructions, cocktailMsg.Image.ToString(), cocktailMsg.Ingredients));
                }
                else
                    cocktailMsg = new CocktailMessage(cachedCocktail.Name, cachedCocktail.ServingGlass, cachedCocktail.Instructions, new Uri(cachedCocktail.Image), cachedCocktail.Ingredients);
            }

            sbyte currentOffset = 0;
            for(double utcOffset = -11; utcOffset < 15; utcOffset++)
            {
                if (DateTime.UtcNow.AddHours(utcOffset).Hour == 17)
                {
                    currentOffset = (sbyte)utcOffset;
                    break;
                }
            }

            var docs = await _RecipientsCosmosService.GetDocuments<RecipientDocument>(
                new QueryDefinition(Constants.Cosmos.Query.CocktailTime.Recipients.GetDocumentsByUtcOffset)
                    .WithParameter("@utcOffset", currentOffset)) ?? new List<RecipientDocument>();

            var tasks = docs.Select(doc => _SMS.Send("+18334503294", doc.PhoneNumber, cocktailMsg.Message)).ToArray();
            Task.WaitAll(tasks);
        }
    }
}
