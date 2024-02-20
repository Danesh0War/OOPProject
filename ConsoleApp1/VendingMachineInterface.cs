using System;
using System.Collections.Generic;
using ConsoleApp1;
using System.Linq;

internal  class VendingMachineInterface // Communication of the machine with user
{
    public static void DisplayBeverages(VendingMachine vendingMachine)
    {
        Console.WriteLine("Our available beverages: \n ");
        for (int i = 0; i < vendingMachine.GetBeveragesCount(); i++)
        {
            Console.WriteLine(vendingMachine[i].ToString() + "\n"); // Here We are using our overriden func in beverage class
        }
    }

    public static void Header() // a little bit of UI
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("                      _ _                                    _     _            \r\n /\\   /\\___ _ __   __| (_)_ __   __ _   _ __ ___   __ _  ___| |__ (_)_ __   ___ \r\n \\ \\ / / _ \\ '_ \\ / _` | | '_ \\ / _` | | '_ ` _ \\ / _` |/ __| '_ \\| | '_ \\ / _ \\\r\n  \\ V /  __/ | | | (_| | | | | | (_| | | | | | | | (_| | (__| | | | | | | |  __/\r\n   \\_/ \\___|_| |_|\\__,_|_|_| |_|\\__, | |_| |_| |_|\\__,_|\\___|_| |_|_|_| |_|\\___|\r\n                                |___/                                           ");
        Console.ResetColor();
        Console.WriteLine("Welcome to our Vending Machine! ");

    }

   
    public static void SelectBeverage(VendingMachine vendingMachine, int choice,StockManager stock)
    {
        if (choice > 0 && choice <= vendingMachine.GetBeveragesCount()) // If in the range
        {
            Console.WriteLine($"You have selected {vendingMachine[choice - 1].Name}. Preparing your beverage...");
            try
            {
                Console.WriteLine(vendingMachine[choice - 1].Prepare(stock));

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please select a valid option.");
        }
    }


}
