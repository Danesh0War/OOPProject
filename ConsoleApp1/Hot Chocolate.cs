using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class HotChocolate : Beverage
    {
        private static List<Ingredient> _requiredIngredients = new List<Ingredient>
        {
            new Ingredient("Cocoa Powder", 1),
            new Ingredient("Sugar", 3),
            new Ingredient("Cup", 1),
            new Ingredient("Milk", 5),
        };

        public HotChocolate(string name, decimal price )
            : base(name, price ) { }

        protected override string AddIngredients()
        {
            StringBuilder result = new StringBuilder();

            foreach (var ingredient in _requiredIngredients)
            {
                switch (ingredient.Name)
                {
                    case "Cocoa Powder":
                        _stockManager.Use(ingredient.Name, 1);
                        result.AppendLine($"Added Cocoa Powder.");
                        break;

                    case "Milk":
                        _stockManager.Use(ingredient.Name, 5);
                        result.AppendLine("Added Milk.");
                        break;

                    case "Sugar":
                        _stockManager.Use(ingredient.Name, 3);
                        result.AppendLine($"Added sugar.");
                        break;

                    default:
                        _stockManager.Use(ingredient.Name, 1);
                        result.AppendLine($"Used 1 {ingredient.Name}.");
                        break;

                }
            }
            return result.ToString();
        }

        protected override string Stir()
        {
            return "Stirring the hot chocolate";
        }
    }
}
