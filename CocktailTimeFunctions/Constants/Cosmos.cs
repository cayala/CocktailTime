using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailTimeFunctions.Constants
{
    public class Cosmos
    {
        public class Query
        {
            public class CocktailTime
            {
                public class Recipients
                {
                    public const string GetDocumentByTimeZone = "select * from Recipients r where r.timezone = @timezone";
                }
            }
        }
    }
}
