using CosmosDB.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB.Interfaces
{
    public interface ICosmosCreate
    {
        Task<T> UpsertDocument<T>(string ID, T document) where T : BaseDocument;
        Task AddDocument<T>(T document) where T : BaseDocument;
    }
}
