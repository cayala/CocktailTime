using CocktailTime.Controllers.DTO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CocktailTime.Json.Deserializers
{
    public class CocktailDtoDeserializer : JsonConverter<CocktailDTO>
    {
        public override CocktailDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            CheckForBeginningToken(ref reader);

            string phoneNumber = null;
            string timeZone = null;
            while (reader.Read()) 
            {
                if (CheckForEndingToken(ref reader))
                    return new CocktailDTO(phoneNumber, timeZone);

                if (reader.TokenType == JsonTokenType.PropertyName) 
                {
                    string propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName) 
                    {
                        case "timezone": timeZone = reader.GetString(); break;
                        case "Timezone": timeZone = reader.GetString(); break;
                        case "timeZone": timeZone = reader.GetString(); break;
                        case "TimeZone": timeZone = reader.GetString(); break;
                        case "phonenumber": phoneNumber = reader.GetString(); break;
                        case "Phonenumber": phoneNumber = reader.GetString(); break;
                        case "phoneNumber": phoneNumber = reader.GetString(); break;
                        case "PhoneNumber": phoneNumber = reader.GetString(); break;
                        default: break;
                    }
                }
            }

            throw new JsonException("JSON object passed in did not have the expected properties");

            static void CheckForBeginningToken(ref Utf8JsonReader reader) 
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException("The beginning token type did not match JsonTokenTYpe.StartObject");
                }
            }
            static bool CheckForEndingToken(ref Utf8JsonReader reader)
                => reader.TokenType == JsonTokenType.EndObject;
        }

        public override void Write(Utf8JsonWriter writer, CocktailDTO value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
