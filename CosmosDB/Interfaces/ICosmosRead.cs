using CosmosDB.Documents;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB.Interfaces
{
    public interface ICosmosRead
    {
        Task<T> GetDocument<T>(string ID) where T : BaseDocument;
        Task<T> GetDocument<T>(QueryDefinition query) where T : BaseDocument;
        Task<List<T>> GetDocuments<T>(QueryDefinition query) where T : BaseDocument;
    }
}
