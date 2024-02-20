using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Tea : Beverage
    {

        // s o l i d

        private static List<Ingredient> _requiredIngredients = new List<Ingredient>
        {
            new Ingredient("Tea leaves", 1),
            new Ingredient("Sugar", 2),
            new Ingredient("Lemon", 1),
            new Ingredient("Cup", 1),

        };

        public Tea(string name, decimal price  )
            : base(name, price) { }

        protected override string AddIngredients()
        {
            StringBuilder result = new StringBuilder();
            foreach (var ingredient in _requiredIngredients)
            {
                switch (ingredient.Name)
                {
                    case "Tea leaves":
                        _stockManager.Use(ingredient.Name, 1);
                        result.AppendLine("Added Tea Leaves.");
                        break;

                    case "Lemon": // In case the name of tcreated instance form class tee there is a word lemon, add lemon. Else don't add. For ex "Black tea with lemon"

                        if (this.Name.ToLowerInvariant().Contains("lemon")) // to find cast to lower in case of "LeMon"
                        {
                            _stockManager.Use(ingredient.Name, 1);
                            result.AppendLine("Added lemon.");
                        }
                        break;
                  
                    case "Sugar":
                        _stockManager.Use(ingredient.Name, 2);
                        result.AppendLine("Added sugar.");
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
            return "Stirring the tea";
        }
    }
}
