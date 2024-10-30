using System;
using System.Collections.Generic;
using System.Security.Principal;
using minibank.Models;

namespace minibank.Services
{
    internal class BankService
    {
        //lista przechowująca konta bankowe
        private List<Account> accounts;
        //lista przechowująca wszystkie transakcje
        private List<Transaction> transactions; 

        public BankService()
        {
            accounts = new List<Account>();
            transactions = new List<Transaction>();
        }

        // metoda przydzielania numeru konta
        private int GenerateAccountNumber()
        {
            //todo when accounts will be saving in DB - then check if random number doesnt exsist - continue, if exsist loop again
            Random generatedAccountNumber = new Random();
            return generatedAccountNumber.Next(100000, 999999); // Generuje 6-cyfrowy numer konta
        }

        //metoda do tworzenia nowego konta
        public Account CreateAccount(string owner, int pinPassword) 
        {
            int accountNumber = GenerateAccountNumber();
            decimal initialBalance = 0.00m;

            var newAccount = new Account(accountNumber, owner, initialBalance, pinPassword);
            accounts.Add(newAccount);
            return newAccount;
        }

        // Metoda do znalezienia konta na podstawie numeru konta i pinu 
        public Account GetAccount(int accountNumber, int pinPassword)
        {
            return accounts.Find(account => account.AccountNumber == accountNumber && account.PinPassword == pinPassword);
        }

        //metoda do wplaty srodkow na konto
        public bool Deposit(int accountNumber, int pinPassword, decimal amount)
        {
            var account = GetAccount(accountNumber, pinPassword);
            if (account != null)
            {
                account.Deposit(amount);
                var transaction = new Transaction(DateTime.Now, account.Owner, amount, "Deposit");
                transactions.Add(transaction);
                return true; // Zwraca true, jeśli depozyt się udał
            }
            return false; // Zwraca false, jeśli konto nie zostało znalezione
        }


        //metoda do wypłaty środków z konta
        public bool Withdraw(int accountNumber, int pinPassword, decimal amount)
        {
            var account = GetAccount(accountNumber, pinPassword);
            if (account != null)
            {
                if (account.Withdraw(amount))
                {
                    var transaction = new Transaction(DateTime.Now, account.Owner, amount, "Withdraw");
                    transactions.Add(transaction);
                    return true;
                }
                else
                {
                    Console.WriteLine("Insufficient funs");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Incorrect login details or account does not exist.");
                return false;
            }
        }

        //metoda do wyświetlania historii transakcji
        public void PrintTransactionHistory()
        {
            Console.WriteLine(" Bank Transaction History:");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Date} | {transaction.Type}: ${transaction.Amount} | {transaction.Owner}");
            }
        }
    }
}
