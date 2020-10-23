using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDB.Documents
{
    public abstract class BaseDocument
    {

        //The CosmosDB SDK has you generate your own GUID even though there should be a method where it autogenerates in cosmos and there isn't a need to have that there.
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; } = Guid.NewGuid().ToString();
    }
}
