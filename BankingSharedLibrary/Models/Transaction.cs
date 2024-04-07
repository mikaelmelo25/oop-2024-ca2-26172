//Name:  Mikael Melo
//student number: 26172

using System;

namespace BankingSharedLibrary.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public decimal FinalBalance { get; set; } 

        // Constructor
        public Transaction(DateTime date, string action, decimal amount, decimal finalBalance)
        {
            Date = date;
            Action = action;
            Amount = amount;
            FinalBalance = finalBalance;
        }

        // Override ToString method for formatting
        public override string ToString()
        {
            return $"{Date}\t{Action}\t{Amount}\t{FinalBalance}";
        }
    }
}
