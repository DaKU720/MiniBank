using System;

namespace minibank.Models
{
    internal class Transaction
    {
        public DateTime Date { get; private set; }
        public string Owner { get; private set; }
        public decimal Amount { get; private set; }
        public string Type { get; private set; } // "Deposit" lub "Withdraw"

        public Transaction(DateTime date, string owner, decimal amount, string type)
        {
            Date = date;
            Owner = owner;
            Amount = amount;
            Type = type;
        }
    }
}
