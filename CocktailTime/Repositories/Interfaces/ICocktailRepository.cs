using CosmosDB.Documents;
using System.Threading.Tasks;

namespace CocktailTime.Repositories.Interfaces
{
    public interface ICocktailRepository
    {
        Task SavePhoneNumber(string phoneNumber, string timeZone, string timezoneCode, bool isDaylightSavings, sbyte utcOffset);
        Task<RecipientDocument> GetDocumentByPhoneNumber(string phoneNumber);
        Task<RecipientDocument> GetDocumentByID(string ID);
        Task DeleteDocument(string ID);
    }
}