using CocktailTimeFunctions.Models;
using Microsoft.Build.Utilities;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CocktailTimeFunctions.Serializer;

namespace CocktailTimeFunctions.HttpClients
{
    public class CocktailDBHttpClient
    {
        //This class should make the httpClient Request
        //This should handle the JSON deserialization of the response
        //TODO: Create custom URL shortner, research needed
        private readonly HttpClient _Client;

        public CocktailDBHttpClient(Uri url) 
        {
            var client = new HttpClient();
            client.BaseAddress = url;
            _Client = client;
        }

        public async Task<CocktailMessage> GetCocktailMessage() 
        {
            return JsonSerializer.Deserialize<CocktailMessage>(await FetchCocktailJson(_Client), SetupDeserializer());

            static async Task<string> FetchCocktailJson(HttpClient client) 
            {
                var response = await client.GetAsync(client.BaseAddress.ToString());
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            static JsonSerializerOptions SetupDeserializer() 
            {
                var deserializerOptions = new JsonSerializerOptions();
                deserializerOptions.Converters.Add(new CocktailMessageSerializer());
                return deserializerOptions;
            }
        }
    }
}
