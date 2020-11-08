using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;
using CocktailTimeFunctions.Models;
using System.Text.Json.Serialization;
using System.Linq;

namespace CocktailTimeFunctions.Serializer
{
    public class CocktailMessageSerializer : JsonConverter<CocktailMessage>
    {
        public override CocktailMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            CheckForBeginningToken(ref reader);

            string name, servingGlass, instructions; Uri image = null; Queue<string> ingredients = null; Queue<string> amounts = null;
            name = servingGlass = instructions = null;

            while (ReadNextJsonToken(ref reader)) 
            {
                if (CheckForEndingToken(ref reader))
                    return new CocktailMessage(name, servingGlass, instructions, image, CreateIngredients(ingredients, amounts));
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString().ToLower();
                    ReadNextJsonToken(ref reader);

                    switch (propertyName)
                    {
                        case "strdrink": name = reader.GetString(); break;
                        case "strglass": servingGlass = reader.GetString(); break;
                        case "strinstructions": instructions = reader.GetString(); break;
                        case "strdrinkthumb": image = new Uri(reader.GetString()); break;
                        default: CheckForIngredient(ref reader, ref ingredients, ref amounts, propertyName); break;
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
            static bool ReadNextJsonToken(ref Utf8JsonReader reader)
                => reader.Read();
            static List<Ingredient> CreateIngredients(Queue<string> ingredientQueue, Queue<string> amountsQueue) 
            {
                List<Ingredient> ingredients = null;
                for (int index = 0; index < ingredientQueue.Count; index++) 
                {
                    ingredients.Add(new Ingredient(ingredientQueue.Dequeue(), amountsQueue.Dequeue()));
                }
                return ingredients;
            }
            static void CheckForIngredient(ref Utf8JsonReader reader, ref Queue<string> ingredients, ref Queue<string> amounts, string propertyName) 
            {
                if (propertyName.Contains("strIngredient"))
                {
                    string ingredient = reader.GetString();
                    if (!String.IsNullOrWhiteSpace(ingredient))
                        ingredients.Enqueue(ingredient);
                }
                else if (propertyName.Contains("strMeasure")) 
                {
                    string amount = reader.GetString();
                    if (!String.IsNullOrWhiteSpace(amount))
                        amounts.Enqueue(amount);
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, CocktailMessage value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
