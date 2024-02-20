using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Coffee : Beverage // The most complex son in the inheritence tree. Wanted to practise learned things and be creative
    {
        private static List<Ingredient> _requiredIngredients = new List<Ingredient> // data field. Common for every bbeverage = list of required ingredients.
                                                                                    // Also by quantity I mean maximum needed amount of each ingredient in order to prepare it 
        {
            new Ingredient("Coffee Beans", 3),
            new Ingredient("Sugar", 2),
            new Ingredient("Cup", 1),
            new Ingredient("Milk", 3),
        };


        public CoffeeStrength Strength { get; private set; } // Practising Enum. Public property because uset has to chose from the predifined set of pissibilies. 

        private double _milk; // data field
        public double Milk  // propetry
        {
            get => this._milk;
            set
            {
                if (value >= 0 && value < 150) // set validation
                    this._milk = value;
                else throw new ArgumentException("Invalid ammount. Must be declared as positive number in range of a cup volume");
            }
        }

        private static List<string> FrothyCoffees = new List<string> // List of drinks from type Coffee that require additional stap in preperation
        {
        "Cappuccino",
        "Macchiato",
        "Latte"
        };

        //ctor
        public Coffee(string name, decimal price, CoffeeStrength strength, double milkAmmount)
            : base(name, price)
        {
            // User must define the strength of coffee and the amount of desired milk
            Strength = strength;
            Milk = milkAmmount;

            if (FrothyCoffees.Contains(name))  // If the created drink is  as one of the item in List, add new method and modify the algoritm of execution! Else will be executed as defined in Beverage ctor
            {
                PrepareDelegate = AddIngredients; 
                PrepareDelegate += AddHotWater;
                PrepareDelegate += AddFroth; // New operation
                PrepareDelegate += Stir;
                PrepareDelegate += FinalizePreparation;
            }

        }

        protected override string AddIngredients()
        {
            StringBuilder result = new StringBuilder(); // Same reason - avoid coping the same string

            foreach (var ingredient in _requiredIngredients) // for specific ingredient from our mannualy defined _requiredIngredients the operation may differ
            {
                switch (ingredient.Name)
                {
                    case "Coffee Beans":
                        _stockManager.Use(ingredient.Name,(int)Strength + 1); // consume different ammount of coffee beans based on chosen strength! (from 1 to 3)
                        result.AppendLine($"Added {(int)Strength + 1} units of coffee beans.");
                        break;

                    case "Milk": // Consume different amount of milk units based on user friendly ml input
                        if (Milk > 0 && Milk < 45)
                        {
                            _stockManager.Use(ingredient.Name, 1);
                            result.AppendLine($"Added {Milk} ml of milk.");

                        }
                        else if (Milk > 45 && Milk < 75)
                        {
                            _stockManager.Use(ingredient.Name, 2);
                            result.AppendLine($"Added {Milk} ml of milk.");
                        }

                        else
                        {
                            _stockManager.Use(ingredient.Name, 3);
                            result.AppendLine($"Added {Milk} ml of milk.");
                        }
                        break;

                    default: // for not specified Ingredient we will consume 1 unit! (In this case it sugar and cups)
                        _stockManager.Use(ingredient.Name, 1);
                        result.AppendLine($"Used 1 {ingredient.Name}.");

                        break;
                }
            }
            return result.ToString(); // Represent String Builder
        }

        protected override string AddHotWater() // override to specialize the func for coffee case 
        {

            Ingredient water = _stockManager.GetIngredient("Water");
            water.Use(1); 
            return $"Boiled water up to 80 degrees and added to your {this.Name}"; // Takes name of created instance
        }

        protected override string Stir()
        {
            return "Stirring the coffee";
        }

        private string AddFroth() // Lehakzif
        {
            return "Frothing your coffee";
        }


        public enum CoffeeStrength : byte
        {
            Lite,
            Medium,
            Strong
        }



    }
}
