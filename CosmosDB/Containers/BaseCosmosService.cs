using CosmosDB.Documents;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDB.Containers
{
    public abstract class BaseCosmosService
    {
        private Container _Container { get; }

        public BaseCosmosService(string databaseName, string containerName, CosmosClient client)
            => _Container = client.GetContainer(databaseName, containerName);

        public virtual async Task<T> GetDocument<T>(string ID) where T : BaseDocument
            => (await _Container.ReadItemAsync<T>(ID, new PartitionKey(ID))).Resource;
        public virtual async Task<T> GetDocument<T>(QueryDefinition query) where T : BaseDocument
        {
            T result = null;
            using var feedIterator = _Container.GetItemQueryIterator<T>(query);

            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    result = item;
                }
            }

            return result;
        }

        public virtual async Task<List<T>> GetDocuments<T>(QueryDefinition query) where T : BaseDocument
        {
            List<T> results = new List<T>();

            using var feedIterator = _Container.GetItemQueryIterator<T>(query);

            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    results.Add(item);
                }
            }

            return results;
        }

        public virtual async Task<T> UpsertDocument<T>(string ID, T document) where T : BaseDocument
            => (await _Container.UpsertItemAsync<T>(document, new PartitionKey(ID))).Resource;

        public virtual async Task AddDocument<T>(T document) where T : BaseDocument
            => await _Container.CreateItemAsync<T>(document, new PartitionKey(document.ID));

        public virtual async Task<T> DeleteDocument<T>(string ID) where T : BaseDocument
            => await _Container.DeleteItemAsync<T>(ID, new PartitionKey(ID));
    }
}
