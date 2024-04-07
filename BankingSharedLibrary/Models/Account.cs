//Name:  Mikael Melo
//student number: 26172

namespace BankingSharedLibrary.Models
{
    public abstract class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; protected set; }
        public string AccountNumber { get; }
        public string Pin { get; }

        public Account(int id, string accountNumber, string pin)
        {
            Id = id;
            AccountNumber = accountNumber;
            Pin = pin;
            Balance = 0;
        }

        public abstract void CreateAccountFile();
        public abstract void CreateTransactionRecord(DateTime date, string action, decimal amount, decimal finalBalance);

        public virtual void AddMoney(decimal amount)
        {
            Balance += amount;
        }

        public void SubtractMoney(decimal amount)
        {
            Balance -= amount;
        }
    }
}