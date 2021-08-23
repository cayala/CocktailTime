using CosmosDB.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CosmosDB.Containers
{
    public class CocktailCacheCosmosService : BaseCosmosService, ICosmosCRUD
    {
        public CocktailCacheCosmosService(string databaseName, string containerName, CosmosClient client) : base(databaseName, containerName, client) { }
    }
}
