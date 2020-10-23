using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CosmosDB;
using CosmosDB.Documents;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SmsService;

namespace CocktailTimeFunctions
{
    public class Send
    {
        private readonly ISMSClient _SMS;
        private readonly ICosmosService _CosmosService;
        private static Queue<TimeZoneInfo> _CurrentTimeZone;

        public Send(ISMSClient sms, ICosmosService cosmosService)
            => (_SMS, _CosmosService) = (sms, cosmosService);

        [FunctionName("Send")]
        public async Task Run([TimerTrigger("0 0 */1 * * *")]TimerInfo myTimer, ILogger log)
        {
            //Pull cocktail from cache
            //If one does not exist then call API to generate new one
            //Generic HttpClient class should abstract out all the setup then use the generic json deserializer
            //Deserialize new cocktail from JSON
            //This should be done in a generic JSON deserializer where you can pass in a type or something then use reflection to match properties and values
            //Then cache deserialized cocktail
            //Generate message from cache
            //If one does not exist, generate new one
            //Make a call to a url shortner for new image


            //TODO: Create way to keep track of timezones where it is 5 o'clock
            //      Could use a dictionary that takes the current hour then compares it to another list or something
            //      Or it could potentially use a queue that refills every hour that then takes the completed timezone and adds it to the end of the queue

            var docs = await _CosmosService.GetDocuments<CocktailDocument>(
                new QueryDefinition(Constants.Cosmos.Query.CocktailTime.Recipients.GetDocumentByTimeZone)
                    .WithParameter("@timezone", "PST")) ?? new List<CocktailDocument>();

            var tasks = new List<Task>();
            foreach(var d in docs)
            {
                tasks.Add(_SMS.Send(String.Empty, d.PhoneNumber, "Hello World!"));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private void SynchTimeZones() 
        {
            var UtcTime = DateTime.UtcNow;
            //var timezoneinfo = TimeZoneInfo.GetSystemTimeZones().Where(x => x.)
            //Then keep shifting timezone forward until it is 5pm/17:00 somewhere
            //It's 5pm in only one timezone so it really just needs to move forward until it finds the proper timezone
            for(int offset = -11; offset < 15; offset++)
            {
                if (UtcTime.Hour == 17)
                    break;
                else
                    UtcTime = UtcTime.AddHours(offset);
            }

            //TimeZoneInfo.
        }
    }
}
