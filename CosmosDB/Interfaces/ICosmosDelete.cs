using CosmosDB.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB.Interfaces
{
    public interface ICosmosDelete
    {
        Task<T> DeleteDocument<T>(string ID) where T : BaseDocument;
    }
}
