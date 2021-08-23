namespace CocktailTimeFunctions.Models
{
    public class Ingredient
    {
        public string Name { get; }
        public string Amount { get; }

        public Ingredient(string name, string amount)
            => (Name, Amount) = (name, amount);
    }
}