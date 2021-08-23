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
                    public const string GetDocumentsByUtcOffset = "select * from Recipients r where r.utcOffset = @utcOffset";
                }

                public class CocktailCache
                {
                    public const string GetCachedCocktail = "select TOP 1 * from CocktailCache cc";
                }
            }
        }
    }
}
