using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace ConsoleApp1
{
    internal class VendingMachine // Beverages Container
    {
        protected List<Beverage> beverages;
        private StockManager _stockManager; // vending machine also needs to communicate with stock manager
        public VendingMachine(StockManager stockManager) // Default types and quantities of ingredients to create all defined beverages
        {
            beverages = new List<Beverage>();
            _stockManager = stockManager;
            _stockManager.AddIngredient(new Ingredient("Coffee Beans", 35));
            _stockManager.AddIngredient(new Ingredient("Milk", 15));
            _stockManager.AddIngredient(new Ingredient("Sugar", 60));
            _stockManager.AddIngredient(new Ingredient("Cup", 5)); // I made it small so in testing we can approach test case fasterer
            _stockManager.AddIngredient(new Ingredient("Water", 30));
            _stockManager.AddIngredient(new Ingredient("Tea leaves", 20));
            _stockManager.AddIngredient(new Ingredient("Lemon", 5)); // I made it small so in testing we can approach test case fasterer
        }

        public Beverage this[string beverageName] // Practising Indexer + additional servise to my container for the user
        {
            get { return beverages.FirstOrDefault(beverage => beverage.Name == beverageName); }
        }

        public Beverage this[int index] // Practising Indexer + additional servise to my container for the user
        {
            get
            {
                if (index >= 0 && index < beverages.Count) // Don't need to create my counter. Lists have build-in. 
                {
                    return beverages[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }
            }
        }

        public int GetBeveragesCount() // keeping track of the number of managed beverages in the container (very usefull for the user also used by me in VendingMachineInterface) 
        {
            return beverages.Count;
        }
        public List<Beverage> GetBeverages() // Container servise
        {
            return beverages;           
        }
        public void AddBeverage(Beverage beverage) // Add beverage to the vending machine
        {
            beverages.Add(beverage);
        }

        public void RemoveBeverage(string name = null, int index = -1) // Remove beverage from the vending machine (by inded or by name)
        {
            Beverage beverageToRemove = null;

            if (name != null)
            {
                beverageToRemove = this[name];
            }
            else if (index != -1)
            {
                beverageToRemove = this[index];
            }

            if (beverageToRemove != null)
            {
                beverages.Remove(beverageToRemove);
            }
            else
            {
                throw new InvalidOperationException($"No beverage found with the name {name} or index {index}");
            }
        }

        public void RemoveBeverage(Beverage beverage) // Remove beverage from the vending machine straightly by passing instance
        {
            if (beverages.Contains(beverage))
            {
                beverages.Remove(beverage);
            }
            else
            {
                throw new InvalidOperationException("Beverage not found in the vending machine.");
            }
        }


        public string PrepareBeverage(string name = null, int index = -1)  
        {
            Beverage beverageToPrepare = null;

            if (name != null)
            {
                beverageToPrepare = this[name];
            }
            else if (index != -1)
            {
                beverageToPrepare = this[index];
            }

            if (beverageToPrepare != null)
            {
                return beverageToPrepare.Prepare(_stockManager);
            }
            else
            {
                throw new InvalidOperationException($"No beverage found with the name {name} or index {index}");
            }
        }

        public string PrepareBeverage(Beverage beverage)
        {

            if (beverages.Contains(beverage))
            {
                return beverage.Prepare(_stockManager);
            }
            else
            {
                throw new InvalidOperationException("Beverage not found in the vending machine.");
            }
        }      

    }
  
}
