using CocktailTime.Repositories.Interfaces;
using CosmosDB;
using CosmosDB.Documents;
using CosmosDB.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace CocktailTime.Repositories
{
    public class CocktailRepository : ICocktailRepository
    {
        private readonly ICosmosCRUD _CosmosDB;
        public CocktailRepository(ICosmosCRUD cosmosDB)
            => _CosmosDB = cosmosDB;
        public Task SavePhoneNumber(string phoneNumber, string timeZone, string timezoneCode, bool isDaylightSavings, sbyte utcOffset)
            =>  _CosmosDB.AddDocument(new RecipientDocument(phoneNumber, timeZone, timezoneCode, isDaylightSavings, utcOffset));

        public Task<RecipientDocument> GetDocumentByPhoneNumber(string phoneNumber)
            =>  _CosmosDB.GetDocument<RecipientDocument>(new QueryDefinition(Constants.Cosmos.Query.CocktailTime.Recipients.GetDocumentByPhoneNumber)
                                                        .WithParameter("@phonenumber", phoneNumber));
        public Task<RecipientDocument> GetDocumentByID(string ID)
            => _CosmosDB.GetDocument<RecipientDocument>(ID);

        public Task DeleteDocument(string ID)
            => _CosmosDB.DeleteDocument<RecipientDocument>(ID);
    }
}
