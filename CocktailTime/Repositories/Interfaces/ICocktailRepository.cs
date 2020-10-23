using CosmosDB.Documents;
using System.Threading.Tasks;

namespace CocktailTime.Repositories.Interfaces
{
    public interface ICocktailRepository
    {
        Task SavePhoneNumber(string phoneNumber, string timeZone);
        Task<CocktailDocument> GetDocumentByPhoneNumber(string phoneNumber);
        Task<CocktailDocument> GetDocumentByID(string ID);
        Task DeleteDocument(string ID);
    }
}