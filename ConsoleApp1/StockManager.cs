using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class StockManager // Container for Ingredients
    {
        private List<Ingredient> ingredients = new List<Ingredient>();

        public void AddIngredient(Ingredient ingredient) // add Ingredient to the stock. Relies on Ingredient func which enlarges the quantity
        {
            ingredients.Add(ingredient);
        }

        public Ingredient GetIngredient(string name) // return desired ingredient
        {
            return ingredients.FirstOrDefault(i => i.Name == name);
        }

        public void Refill(string name, int quantity) // If ingredient doesnt exists in the container, create a new one and add new single ingredient. If exists, enlarge it's quantity!
        {
            var ingredient = GetIngredient(name);
            if (ingredient == null)
            {
                ingredient = new Ingredient(name, quantity);
                AddIngredient(ingredient);
            }
            else
            {
                ingredient.Add(quantity);
            }
        }

        public void GlobalRefill() // Add a certain quantity of each ingredient so we restore the initial defined quantity!
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Add(ingredient.InitialQuantity - ingredient.Quantity);
            }
        }

        public void Use(string name, int amount) // Take from quantity of ingredient. Relies on Ingredient func which reduces the quantity
        {
            {
                var i = GetIngredient(name);
                i.Use(amount);
            }


        }

    }
}
