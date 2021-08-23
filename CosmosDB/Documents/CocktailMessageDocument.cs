using CocktailTimeFunctions.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CosmosDB.Documents
{
    public class CocktailMessageDocument: BaseDocument
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "servingGlass")]
        public string ServingGlass { get; set; }
        [JsonProperty(PropertyName = "instructions")]
        public string Instructions { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
        [JsonProperty(PropertyName = "ingredients")]
        public List<Ingredient> Ingredients { get; set; }
        public CocktailMessageDocument(string name, string servingGlass, string instructions, string image, List<Ingredient> ingredients)
            => (Name, ServingGlass, Instructions, Image, Ingredients) = (name, servingGlass, instructions, image, ingredients);
    }
}
