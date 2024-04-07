//Name:  Mikael Melo
//student number: 26172

using System.Collections.Generic;

namespace BankingSharedLibrary.Models
{
    public class Customer : User
    {
        public int Id { get; set; }
        public SavingsAccount SavingsAccount { get; set; }
        public CurrentAccount CurrentAccount { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        public Customer(int id, string firstName, string lastName, string accountNumber, string pin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AccountNumber = accountNumber;
            Pin = pin;
            SavingsAccount = new SavingsAccount(id, accountNumber, pin);
            CurrentAccount = new CurrentAccount(id, accountNumber, pin);
            Accounts.Add(SavingsAccount);
            Accounts.Add(CurrentAccount);
        }

        public bool Login(string pin)
        {
            return pin == Pin;
        }

        public List<Transaction> RetrieveTransactionHistory(string accountType)
        {
            if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                return SavingsAccount.TransactionHistory;
            }
            else if (accountType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                return CurrentAccount.TransactionHistory;
            }
            else
            {
                throw new ArgumentException("Invalid account type.");
            }
        }

        public void AddMoney(string accountType, decimal amount)
        {
            if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                SavingsAccount.AddMoney(amount);
            }
            else if (accountType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                CurrentAccount.AddMoney(amount);
            }
            else
            {
                throw new ArgumentException("Invalid account type.");
            }
        }

        public void SubtractMoney(string accountType, decimal amount)
        {
            if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                SavingsAccount.SubtractMoney(amount);
            }
            else if (accountType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                CurrentAccount.SubtractMoney(amount);
            }
            else
            {
                throw new ArgumentException("Invalid account type.");
            }
        }
    }
}