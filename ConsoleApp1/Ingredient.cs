using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Ingredient // I thooght that every Ingredient deserve to be handled as object too. 
    {
        //properties
        public string Name { get; } // only to show
        public int Quantity { get; private set; } // could be modified within the class
        public int InitialQuantity { get; } // only to show (Will be used to keep track of initially defined amount of each ingredient so then could easily refill all the ingredient to the normal state)


        public Ingredient(string name, int quantity) 
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$")) // validation
            {
                throw new ArgumentException("Invalid Ingredient. Ingredient should consist only of Latin letters.");
            }
            else Name = name;

            Quantity = quantity;
            InitialQuantity = quantity; 
        }

        public void Use (int  quantity)
        {
            if (Quantity < quantity) // I need to handle the case when beverage tries to take the insufficient amount of ingredient
            {
                throw new ArgumentException($"Not enough {Name} in the stock ");
            }
            Quantity -= quantity;
        }


        public void Add(int quantity) => Quantity += quantity;

    }
}