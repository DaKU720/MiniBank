using System;

namespace minibank.Models
{
    internal class Account
    {
        // właściwości konta
        public int AccountNumber { get; private set; }
        public string Owner { get; private set; }
        public decimal Balance { get; private set; }
        //moje nowe:
        public int PinPassword { get; private set; }

        // konstruktor klasy Account
        public  Account(int accountNumber, string owner, decimal initialBalance, int pinPassword)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance; //default 0.00,-
            PinPassword = pinPassword;
        }

        // metoda do wpłaty środków
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount; //dodaje wpłaconą gotówkę kwotę na konto
            }
            else
            {
                throw new ArgumentException("The deposit amount must be positive");
            }
        }

        // metoda do wypłaty środków
       public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance) //sprawdza, czy saldo konta (Balance) jest wystarczające, aby zrealizować wypłatę. Jeśli kwota amount jest większa niż saldo, wypłata się nie powiedzie.
            {
                Balance -= amount; //zmniejsza saldo o wypłacaną kwotę.
                return true;
            }
            return false; // brak wystarczających środków na koncie do wypłacenia
        }
    }
}
