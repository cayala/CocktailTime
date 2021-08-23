using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDB.Interfaces
{
    public interface ICosmosCRUD: ICosmosCreate, ICosmosRead, ICosmosDelete
    {
    }
}
