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

      // Simple menu loop so the app doesn't just run once and exit.
      // Keep asking until the user picks "Exit".
      bool running = true;
      while (running)
      {
        Console.WriteLine("\n1) Add  2) Remove  3) Edit  4) List  5) Exit");
        Console.Write("Choose: ");
        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            AddExpense(Expenses);
            break;
          case "2":
            RemoveExpense(Expenses);
            break;
          case "3":
            EditExpense(Expenses);
            break;
          case "4":
            ListExpenses(Expenses);
            break;
          case "5":
            running = false;
            break;
          default:
            Console.WriteLine("Not a valid option, try again.");
            break;
        }
      }
    }

    static void ListExpenses(List<ExpenseType> expensesArray)
    {
      if (expensesArray.Count == 0)
      {
        Console.WriteLine("No expenses yet.");
        return;
      }

      for (int i = 0; i < expensesArray.Count; i++)
      {
        Console.WriteLine($"[{i}] {expensesArray[i]}");
      }
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

    // TODO: implement this yourself. Steps to follow:
    // 1. Call ListExpenses(expensesArray) so the user can see what index to pick.
    //    If the list is empty, ListExpenses already prints a message — just return early.
    // 2. Prompt: "Enter the index of the expense to remove: " and read the input.
    // 3. Parse it with int.TryParse (same pattern as double.TryParse above for amount).
    // 4. Validate the parsed index is within range: 0 <= index < expensesArray.Count.
    //    If it's out of range or didn't parse, print an error and return (don't crash).
    // 5. Remove it with expensesArray.RemoveAt(index).
    //    (records are just data, so RemoveAt is enough — no need to "find" anything first)
    static void RemoveExpense(List<ExpenseType> expensesArray)
    {
      ListExpenses(expensesArray);
      if (expensesArray.Count == 0)
      {
        return;
      }

      Console.Write("Enter the index of the expense to remove: ");
      string input = Console.ReadLine() ?? "";

      if (!int.TryParse(input, out int index) || index < 0 || index >= expensesArray.Count)
      {
        Console.WriteLine("Invalid index.");
        return;
      }

      expensesArray.RemoveAt(index);
      Console.WriteLine("Expense removed.");
    }

    // TODO: implement this yourself. Steps to follow:
    // 1. Same as Remove: ListExpenses(expensesArray), then ask for an index, validate it.
    // 2. Records are immutable (that's what `record` gives you), so you can't mutate
    //    expensesArray[index] in place. Instead you build a NEW ExpenseType and
    //    overwrite the slot: expensesArray[index] = new(...).
    // 3. The easiest way to "edit" is to reuse the old values as defaults and only
    //    overwrite the field(s) the user actually wants to change, e.g.:
    //      ExpenseType old = expensesArray[index];
    //      Console.Write($"Enter new name (blank to keep '{old.Expense}'): ");
    //      string input = Console.ReadLine() ?? "";
    //      string newExpense = input == "" ? old.Expense : input;
    //    Repeat that pattern for Amount (parse with TryParse), Description, etc.
    // 4. Once you've collected the new values, do:
    //      expensesArray[index] = old with { Expense = newExpense, Amount = newAmount, ... };
    //    ("with" is C# record syntax — it copies `old` and only changes the fields you list)
    static void EditExpense(List<ExpenseType> expensesArray)
    {
      ListExpenses(expensesArray);
      if (expensesArray.Count == 0)
      {
        return;
      }

      Console.Write("Enter the index of the expense to edit: ");
      string indexInput = Console.ReadLine() ?? "";

      if (!int.TryParse(indexInput, out int index) || index < 0 || index >= expensesArray.Count)
      {
        Console.WriteLine("Invalid index.");
        return;
      }

      ExpenseType old = expensesArray[index];

      Console.Write($"Enter new name (blank to keep '{old.Expense}'): ");
      string nameInput = Console.ReadLine() ?? "";
      string newExpense = nameInput == "" ? old.Expense : nameInput;

      Console.Write($"Enter new amount (blank to keep '{old.Amount}'): ");
      string amountInput = Console.ReadLine() ?? "";
      double newAmount = old.Amount;
      if (amountInput != "")
      {
        if (double.TryParse(amountInput, out double parsedAmount))
        {
          newAmount = parsedAmount;
        }
        else
        {
          Console.WriteLine("Invalid amount, keeping the old value.");
        }
      }

      Console.Write($"Enter new description (blank to keep '{old.Description}'): ");
      string descriptionInput = Console.ReadLine() ?? "";
      string? newDescription = descriptionInput == "" ? old.Description : descriptionInput;

      expensesArray[index] = old with { Expense = newExpense, Amount = newAmount, Description = newDescription };
      Console.WriteLine("Expense updated.");
    }
  }
}
