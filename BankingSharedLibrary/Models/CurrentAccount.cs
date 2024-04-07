//Name:  Mikael Melo
//student number: 26172

using System;
using System.Collections.Generic;

namespace BankingSharedLibrary.Models
{
    public class CurrentAccount : Account
    {
        public List<Transaction> TransactionHistory { get; } // Add TransactionHistory property

        public CurrentAccount(int id, string accountNumber, string pin) : base(id, accountNumber, pin)
        {
            CreateCurrentFile();
            TransactionHistory = new List<Transaction>();
        }

        public void CreateCurrentFile()
        {
            // Logic to create current account file
            string fileName = $"{AccountNumber}-current.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Create the file if it doesn't exist
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                Console.WriteLine("Current account file created successfully.");
            }
            else
            {
                Console.WriteLine("Current account file already exists.");
            }
        }

        // Implementing the abstract methods from the Account class
        public override void CreateAccountFile()
        {
            CreateCurrentFile();
        }

        public override void CreateTransactionRecord(DateTime date, string action, decimal amount, decimal finalBalance)
        {
            // Logic to create transaction record for current account
            string fileName = $"{AccountNumber}-current.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Append transaction details to the file
            string transactionDetails = $"{date}\t{action}\t{amount}\t{finalBalance}";
            File.AppendAllText(filePath, transactionDetails + Environment.NewLine);

            // Add transaction to TransactionHistory
            TransactionHistory.Add(new Transaction(date, action, amount, finalBalance));
        }
    }
}
