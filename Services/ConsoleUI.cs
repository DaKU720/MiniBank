using System;
using minibank.Services;

namespace MiniBank.UI
{
    public class ConsoleUI
    {
        private readonly BankService bankService;

        public ConsoleUI()
        {
            bankService = new BankService();
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== Welcome to MiniBank =====");
                Console.ResetColor();
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Withdraw();
                        break;
                    case "4":
                        ViewTransactionHistory();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void CreateAccount()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Create Account ===");
            Console.ResetColor();
            Console.Write("Enter full owner name: ");
            string owner = Console.ReadLine();

            Console.Write("Enter new PIN: ");
            int pinPassword = int.Parse(Console.ReadLine());

            var account = bankService.CreateAccount(owner, pinPassword);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Account created successfully!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Owner: {account.Owner}");
            Console.WriteLine($"Initial Balance: ${account.Balance:F2}");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void Deposit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Deposit ===");
            Console.ResetColor();
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter PIN password: ");
            int pinPassword = int.Parse(Console.ReadLine());

            var account = bankService.GetAccount(accountNumber, pinPassword);
            if (account != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Your current balance is: ${account.Balance:F2}");
                Console.ResetColor();

                Console.Write("Enter deposit amount: $");
                decimal amount = decimal.Parse(Console.ReadLine());

                bool result = bankService.Deposit(accountNumber, pinPassword, amount);
                if (result)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Deposit successful for Account {account.AccountNumber}.");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Updated Balance: ${account.Balance:F2}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Deposit failed. Please try again.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect login details or account does not exist.");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void Withdraw()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Withdraw ===");
            Console.ResetColor();
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter PIN password: ");
            int pinPassword = int.Parse(Console.ReadLine());

            var account = bankService.GetAccount(accountNumber, pinPassword);
            if (account != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Your current balance is: ${account.Balance:F2}");
                Console.ResetColor();

                Console.Write("Enter withdraw amount: $");
                decimal amount = decimal.Parse(Console.ReadLine());

                bool result = bankService.Withdraw(accountNumber, pinPassword, amount);
                if (result)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Withdraw successful for Account {account.AccountNumber}.");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Updated Balance after withdraw: ${account.Balance:F2}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Withdrawal failed. Please check your balance or account details.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect login details or account does not exist.");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewTransactionHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Transaction History ===");
            Console.ResetColor();
            bankService.PrintTransactionHistory();
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}
