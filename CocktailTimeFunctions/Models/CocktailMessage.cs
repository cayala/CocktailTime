using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailTimeFunctions.Models
{
    public class CocktailMessage
    {
        public string Name { get; set; }
        public string ServingGlass { get; set; }
        public string Instructions { get; set; }
        public Uri Image { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public readonly string Message;
        public CocktailMessage(string name, string servingGlass, string instructions, Uri image, List<Ingredient> ingredients) 
        {
            Name = name;
            ServingGlass = servingGlass;
            Instructions = instructions;
            Image = image ?? new Uri(String.Empty);
            Ingredients = ingredients;
            Message = $"It's cocktail time! Today's drink is: {NullCheckString(Name)}! The ingredients are: {ConcatIngredients(Ingredients)}. This is then served in a {NullCheckString(ServingGlass)}. Here's how to make it; {NullCheckString(Instructions)} {Image.ToString()}";

            static string ConcatIngredients(List<Ingredient> ingredients) 
            {
                StringBuilder sb = new StringBuilder();

                for (int index = 0; index < ingredients.Count - 1; index++) 
                {
                    sb.Append($"{ingredients[index].Amount} of {ingredients[index].Name}, ");
                }
                sb.Append($"and {ingredients[ingredients.Count - 1].Amount} of {ingredients[ingredients.Count - 1].Name}");
                return sb.ToString();
            }
            static string NullCheckString(string str) 
                => !String.IsNullOrWhiteSpace(str) ? str : String.Empty;
        }
    }
}
