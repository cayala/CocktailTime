using CocktailTime.Repositories.Interfaces;
using CosmosDB;
using CosmosDB.Documents;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace CocktailTime.Repositories
{
    public class CocktailRepository : ICocktailRepository
    {
        private readonly ICosmosService _CosmosDB;
        public CocktailRepository(ICosmosService cosmosDB)
            => _CosmosDB = cosmosDB;
        public Task SavePhoneNumber(string phoneNumber, string timeZone)
            =>  _CosmosDB.AddDocument(new CocktailDocument(phoneNumber, timeZone));

        public Task<CocktailDocument> GetDocumentByPhoneNumber(string phoneNumber)
            =>  _CosmosDB.GetDocument<CocktailDocument>(new QueryDefinition(Constants.Cosmos.Query.CocktailTime.Recipients.GetDocumentByPhoneNumber)
                                                        .WithParameter("@phonenumber", phoneNumber));
        public Task<CocktailDocument> GetDocumentByID(string ID)
            => _CosmosDB.GetDocument<CocktailDocument>(ID);

        public Task DeleteDocument(string ID)
            => _CosmosDB.DeleteDocument<CocktailDocument>(ID);
    }
}
