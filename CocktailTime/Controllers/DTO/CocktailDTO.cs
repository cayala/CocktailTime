using CocktailTime.Repositories.Interfaces;
using CosmosDB.Documents;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CocktailTime.Json.Deserializers;
using System.ComponentModel.DataAnnotations;

namespace CocktailTime.Controllers.DTO
{
    [JsonConverter(typeof(CocktailDtoDeserializer))]
    public class CocktailDTO
    {
        //Read: reason why this works vs. using a readonly is beacause this is considered a property, the other is considered a field
        
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get;}
        [DataType(DataType.Text)]
        public string TimeZone { get;}
        [DataType(DataType.Text)]
        public string TimeZoneCode { get; }
        public bool IsDaylightSavings { get; }
        public sbyte UtcOffSet { get; }


        public CocktailDTO(string phoneNumber, string timeZone, string timeZoneCode, bool isDaylightSavings, sbyte utcOffset)
            => (PhoneNumber, TimeZone, TimeZoneCode, IsDaylightSavings, UtcOffSet) = (phoneNumber,timeZone, timeZoneCode, isDaylightSavings, utcOffset);

        public Task SavePhoneNumber(ICocktailRepository repo)
            => repo.SavePhoneNumber(PhoneNumber, TimeZone, TimeZoneCode, IsDaylightSavings, UtcOffSet);

        public Task<RecipientDocument> GetPhoneNumber(ICocktailRepository repo)
            => repo.GetDocumentByPhoneNumber(PhoneNumber);
        public static Task DeletePhoneNumber(ICocktailRepository repo, string ID)
            => repo.DeleteDocument(ID);
    }
}
