using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal abstract class Beverage // abstract class for creating the real deriving class uniformly
    {
        //data fields
        private string _name;
        private decimal _price;
        protected List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        protected StockManager _stockManager; // beverages must communicate with StockManager in order to keed of the Ingredient quantity and modify it

        private const decimal _taxRate = 0.17M; // Training the learned const. Price will be adjusted accordingly to Israel tax. 

        //Properties
        public string Name
        {
            get => this._name;
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z\s]+$")) // Validation when trying to assign value
                    this._name = value;
                else throw new ArgumentException("Invalid Name. Name should consist only of latin letters.");
            }
        }

        public decimal Price // decimal is a new data type I learned that is used in money operation, it holds more numbers afer whole part to be more accurate. Consumes more memory and for our case even float will be fine, but we are practising
        {
            get => this._price;
            set
            {
                if (value > 0) 
                    this._price = value + (value * _taxRate); // Automatically add tax to defined price
                else throw new ArgumentException("Invalid Price. Must be declared as positive number");
            }
        }


        protected delegate string BeverageDelegate(); // Training the learned delegates. Defined nside class becauses will be used only as part of beverage objects. 
        //Thought this will be a good idea because I want to predefine the preparing alghoritm for all the inheritance tree
        protected BeverageDelegate PrepareDelegate;

        //ctor
        protected Beverage(string name, decimal price)
        {
            Name = name;
            Price = price;

            //When creating an instance from  inheritance tree, the executional order will be predifined 
            PrepareDelegate = AddIngredients;
            PrepareDelegate += AddHotWater;
            PrepareDelegate += Stir;
            PrepareDelegate += FinalizePreparation;
        }


        public string Prepare(StockManager stockManager) // The unified, common and predifined method for preparing the beverage
        {
            _stockManager = stockManager; // we will use the Ingredients from the chosen stock Manager!
            StringBuilder result = new StringBuilder(); // Practising chosen mechanic to avoid creating new string insted of modifing the exiting one
            foreach (BeverageDelegate step in PrepareDelegate.GetInvocationList()) // for every func in list of func in delegate
            {
                result.AppendLine(step.Invoke()); // Invoke each step and add the result (string) to string builder
            }
            return result.ToString(); // Represent our StringBuilder
        }


        protected abstract string AddIngredients(); // abstract because required to be implemented in each beverage and will be unique for each beverage. 

        protected virtual string AddHotWater() // default implementation for eacg beverage, but may vary in deriving classes so virtual
        {
            Ingredient water = _stockManager.GetIngredient("Water"); // No mater which beverage, by default we will use 1 unit of Water Ingredient from the stock
            water.Use(1);  // assuming each drink uses 1 unit of water
            return $"Added 90 degrees hot water.";
        }
        protected abstract string Stir(); 
        protected virtual string FinalizePreparation() // Final message indicating Invoke was successfull and prepare func ended successfully. May vary in deriving classes
        {
            string finalMess = $"All the steps have been successfully completed, enjoy your {this.Name}!";
            return finalMess;
        }


        public override string ToString() // According to requirements. Also will be used wor beverage representation
        {
            CultureInfo ils = new CultureInfo("en-US"); // Practising string foramtting 
            string formattedPrice = string.Format(ils, "{0:C}", Price); // Practising string foramtting

            return $"Name: {Name}\nPrice: {formattedPrice}\n";
        }

        public override bool Equals(object obj) // According to requirements. 
        {
            if (obj == null) return false;
            if (obj is Beverage beverage)
            {
                if (this.Name == beverage.Name && this.Price == beverage.Price && this.Ingredients.SequenceEqual(beverage.Ingredients))
                {
                    return true;
                }                       
            }
            return false;
        }



    }
}
