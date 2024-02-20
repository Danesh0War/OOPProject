using System;
using static ConsoleApp1.Coffee;

namespace ConsoleApp1
{
    internal class Manager // Our Program class. Simulates the machine in work
    {
        static void Main(string[] args)
        {
            StockManager stockManager = new StockManager(); // define one stockManager (maybe several vending machine depend on 1 stock and vise verse

            Beverage Cappuccino = new Coffee("Cappuccino", 5m, CoffeeStrength.Lite, 50); // For Cappchino we will take 1 unit of coffee beans, 2 units of milk and perform addition opearation - frothing
            Beverage Americano = new Coffee("Americano", 4m, CoffeeStrength.Strong, 0); // For Americano we will take 3 units of coffee beans, no milk and no additional operation
            Beverage EarlGrey = new Tea("EarlGrey", 3.50m);
            Beverage ChristmasCharm = new HotChocolate("ChristmasCharm", 7m);

            VendingMachine vm1 = new VendingMachine(stockManager); // define our venidng machine
            //add created beverages
            vm1.AddBeverage(Cappuccino);
            vm1.AddBeverage(Americano);
            vm1.AddBeverage(ChristmasCharm);
            vm1.AddBeverage(EarlGrey);
            //remove created beverage
            vm1.RemoveBeverage(ChristmasCharm);


            VendingMachineInterface.Header(); // UI)
            while (true) // so we can check that our machine propperly manages the ingredient quantity consumtion, etc. 
            {

                VendingMachineInterface.DisplayBeverages(vm1); // list beverages

                int choice;
                while (!Int32.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > vm1.GetBeveragesCount()) // choise range validation 
                {
                    Console.WriteLine("Invalid input, please enter a valid number.");
                }
                try // try because we possilby could came across exception
                {
                    VendingMachineInterface.SelectBeverage(vm1, choice, stockManager); // machine tries to prepare selected drink. 
                }
                catch (Exception e) // Possibly catch "Not enough ingredient in the stock exception
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please call manager");
                    Console.ForegroundColor = ConsoleColor.White;
                    // I thought about more complex action like allow user to select another beverage instead or to activate the already implemented func Refill or GlobalRefill
                    // These could be future improvements. We already did a lot of extra features and learned a lot. 
                }
            }
      
        }
    }
}
