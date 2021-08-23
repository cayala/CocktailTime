using CosmosDB.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CosmosDB.Containers
{
    public class RecipientsCosmosService : BaseCosmosService, ICosmosCRUD
    {
        public RecipientsCosmosService(string databaseName, string containerName, CosmosClient client) : base(databaseName, containerName, client) { }
    }
}
