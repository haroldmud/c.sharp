using System;
using System.Collections.Generic;



namespace ExpenseTracker
{
  class MyExpense
  {
    public record ExpenseType(string Expense, double Amount, string Currency, DateOnly Date, string? Description, bool IsPaid);
    static void Main()
    {
      List<ExpenseType> Expenses = new List<ExpenseType>();
      //  Expenses.Add(new("shoes", 25.4, "USD", DateOnly.FromDateTime(DateTime.Now), "nothin", true ));
      AddExpense(Expenses);
      Console.WriteLine(Expenses[0]);
    }

    static void AddExpense(List<ExpenseType> expensesArray)
    {
      bool isRejected = false;
      Console.WriteLine("\n Add a New Expense \n \n");

      Console.Write("Enter name: ");
      string expense = Console.ReadLine() ?? "Unknown";

      Console.Write("Enter amount in $: ");
      string amount = Console.ReadLine() ?? "Unknown";
      double parsedAmount = 0;
      if (double.TryParse(amount, out double result))
      {
        parsedAmount = result;
      }
      else
      {
        Console.WriteLine("The Amount is Unsupported, You try again:");
        string retry = Console.ReadLine() ?? "Unknown";
        if (double.TryParse(retry, out double resulta))
        {
          parsedAmount = resulta;
        }
        else
        {
          isRejected = true;
          Console.WriteLine("I CAN SEE YOU ARE NOT SERIOUS ABOUT THIS");
        }
      }

        
        string currency = "USD";
        DateOnly entryDate = DateOnly.FromDateTime(DateTime.Now);

      
        Console.Write("Enter Description: ");
        string description = Console.ReadLine() ?? "No Description";

        // Console.Write("Is Whatever purchased paid ? (Y/n) ");
        bool isPaid = true;
        // ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        // if(keyInfo.key == ConsoleKey.Y)
        // {
        //   Console.WriteLine("Yes");
        //   isPaid = true;
        // }
        // if(keyInfo.Key == ConsoleKey.N)
        // {
        //   Console.WriteLine("No");
        //   isPaid = false;
        // }

        expensesArray.Add(new(expense, parsedAmount, currency, entryDate, description, isPaid));
    }
  }
}
