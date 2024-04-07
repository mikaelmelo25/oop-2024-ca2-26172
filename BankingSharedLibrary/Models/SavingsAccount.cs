//Name:  Mikael Melo
//student number: 26172

using System;
using System.Collections.Generic;

namespace BankingSharedLibrary.Models
{
    public class SavingsAccount : Account


    {


        public List<Transaction> TransactionHistory { get; } // Add TransactionHistory property

        public SavingsAccount(int id, string accountNumber, string pin) : base(id, accountNumber, pin)
        {
            CreateSavingsFile(); 
            TransactionHistory = new List<Transaction>(); // Initializing TransactionHistory
        }

        public void CreateSavingsFile()
        {
            // Logic to create savings account file
            string fileName = $"{AccountNumber}-savings.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Create the file if it doesn't exist
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                Console.WriteLine("Savings account file created successfully.");
            }
            else
            {
                Console.WriteLine("Savings account file already exists.");
            }
        }

        // Implementing the abstract methods from the Account class
        public override void CreateAccountFile()
        {
            CreateSavingsFile();
        }

        public override void CreateTransactionRecord(DateTime date, string action, decimal amount, decimal finalBalance)
        {
            // create transaction record for savings account
            string fileName = $"{AccountNumber}-savings.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Append transaction details to the file
            string transactionDetails = $"{date}\t{action}\t{amount}\t{finalBalance}";
            File.AppendAllText(filePath, transactionDetails + Environment.NewLine);

            // Add transaction to TransactionHistory
            TransactionHistory.Add(new Transaction(date, action, amount, finalBalance));
        }

       
        public override void AddMoney(decimal amount)
        {
            // Logic to add money to the savings account
           
            Balance += amount;
            CreateTransactionRecord(DateTime.Now, "Deposit", amount, Balance);
        }
    }
}
