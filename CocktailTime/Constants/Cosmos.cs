using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailTime.Constants
{
    public class Cosmos
    {
        public class Query
        {
            public class CocktailTime
            {
                public class Recipients
                {
                    public const string GetDocumentByPhoneNumber = "select * from Recipients r where r.phonenumber = @phonenumber";
                }
            }
        }
    }
}
