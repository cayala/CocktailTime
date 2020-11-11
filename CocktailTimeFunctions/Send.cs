using CocktailTimeFunctions.HttpClients;
using CosmosDB;
using CosmosDB.Documents;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SmsService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailTimeFunctions
{
    public class Send
    {
        private readonly ISMSClient _SMS;
        private readonly ICosmosService _CosmosService;
        private readonly CocktailDBHttpClient _HttpClient;

        public Send(ISMSClient sms, ICosmosService cosmosService, CocktailDBHttpClient httpClient)
            => (_SMS, _CosmosService, _HttpClient) = (sms, cosmosService, httpClient);

        [FunctionName("Send")]
        public async Task Run([TimerTrigger("0 0 17 * * *")]TimerInfo myTimer, ILogger log)
        //public async Task Run([TimerTrigger("10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            var cocktailMsg = await _HttpClient.GetCocktailMessage();

            //TODO: Create way to keep track of timezones where it is 5 o'clock
            //      Could use a dictionary that takes the current hour then compares it to another list or something
            //      Or it could potentially use a queue that refills every hour that then takes the completed timezone and adds it to the end of the queue

            var docs = await _CosmosService.GetDocuments<CocktailDocument>(
                new QueryDefinition(Constants.Cosmos.Query.CocktailTime.Recipients.GetDocumentByTimeZone)
                    .WithParameter("@timezone", "PDT")) ?? new List<CocktailDocument>();

            var tasks = new List<Task>();
            foreach(var d in docs)
            {
                tasks.Add(_SMS.Send("+18334503294", "+1" + d.PhoneNumber, cocktailMsg.Message));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
