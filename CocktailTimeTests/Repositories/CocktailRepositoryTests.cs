using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CosmosDB;
using CosmosDB.Documents;
using CosmosDB.Interfaces;
using Microsoft.Azure.Cosmos;
using Moq;
using Xunit;

namespace CocktailTimeTests.Repositories
{
    public class CocktailRepositoryTests
    {
        //public const

        [Fact]
        public void GetsTheDocumentByID() 
        {
        
        }

        private class MockCosmosDB : ICosmosCRUD
        {
            private FakeCosmosDB _db = new FakeCosmosDB();

            public Task AddDocument<T>(T document) where T : BaseDocument
            {
                return Task.Run(() => 
                {
                    _db.Documents.Add(new FakeCosmosDocument());
                });
            }

            public Task<T> DeleteDocument<T>(string ID) where T : BaseDocument
            {
                throw new NotImplementedException();
            }

            public Task<T> GetDocument<T>(string ID) where T : BaseDocument
            {
                throw new NotImplementedException();
            }

            public Task<T> GetDocument<T>(QueryDefinition query) where T : BaseDocument
            {
                throw new NotImplementedException();
            }

            public Task<List<T>> GetDocuments<T>(QueryDefinition query) where T : BaseDocument
            {
                throw new NotImplementedException();
            }

            public Task<T> UpsertDocument<T>(string ID, T document) where T : BaseDocument
            {
                throw new NotImplementedException();
            }
        }

        private class FakeCosmosDB
        {
            public List<FakeCosmosDocument> Documents { get; set; }
        }

        private class FakeCosmosDocument : BaseDocument
        {
            public string value { get; set; }
        }
    }
}
