using CocktailTime.Repositories.Interfaces;
using CosmosDB.Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CocktailTime.Controllers.DTO
{
    public class CocktailDTO
    {
        //Read: reason why this works vs. using a readonly is beacause this is considered a property, the other is considered a field
        public string PhoneNumber { get;}
        public string TimeZone { get;}
        public CocktailDTO(string phoneNumber, string timeZone)
            => (PhoneNumber, TimeZone) = (phoneNumber,timeZone);

        public Task SavePhoneNumber(ICocktailRepository repo)
            => repo.SavePhoneNumber(PhoneNumber, TimeZone);

        public Task<CocktailDocument> GetPhoneNumber(ICocktailRepository repo)
            => repo.GetDocumentByPhoneNumber(PhoneNumber);
        public static Task DeletePhoneNumber(ICocktailRepository repo, string ID)
            => repo.DeleteDocument(ID);
    }
}
