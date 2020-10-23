using Newtonsoft.Json;

namespace CosmosDB.Documents
{
    public class CocktailDocument: BaseDocument
    {
        [JsonProperty(PropertyName = "phonenumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "timezone")]
        public string TimeZone { get; set; }

        public CocktailDocument(string phoneNumber, string timeZone)
            => (PhoneNumber, TimeZone) = (phoneNumber, timeZone);

        public CocktailDocument() { }
    }
}
