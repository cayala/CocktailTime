using CocktailTime.Repositories.Interfaces;
using CosmosDB.Documents;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CocktailTime.Json.Deserializers;

namespace CocktailTime.Controllers.DTO
{
    [JsonConverter(typeof(CocktailDtoDeserializer))]
    public class CocktailDTO
    {
        //Read: reason why this works vs. using a readonly is beacause this is considered a property, the other is considered a field
        public string PhoneNumber { get;}
        public string TimeZone { get;}
        
        public CocktailDTO(string phoneNumber, string timeZone)
            => (PhoneNumber, TimeZone) = (phoneNumber,timeZone);

        public Task SavePhoneNumber(ICocktailRepository repo)
            => repo.SavePhoneNumber(PhoneNumber, TimeZone);

        public Task<CocktailDocument> GetPhoneNumber(ICocktailRepository repo)
            => repo.GetDocumentByPhoneNumber(PhoneNumber);
        public static Task DeletePhoneNumber(ICocktailRepository repo, string ID)
            => repo.DeleteDocument(ID);
    }
}
