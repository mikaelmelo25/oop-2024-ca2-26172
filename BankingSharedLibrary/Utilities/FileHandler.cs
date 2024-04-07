//Name:  Mikael Melo
//student number: 26172

using System;
using System.IO;

namespace BankingSharedLibrary.Services
{
    public class FileHandler
    {
        private readonly string basePath;

        public FileHandler(string basePath)
        {
            this.basePath = basePath;
        }

        public void CreateCustomersFile()
        {
            string customersFilePath = Path.Combine(basePath, "customers.txt");
            if (!File.Exists(customersFilePath))
            {
                File.Create(customersFilePath).Close();
            }
        }

        public void CreateAccountTransactionFile(string accountNumber, string accountType)
        {
            string fileName = $"{accountNumber}-{accountType.ToLower()}.txt";
            string filePath = Path.Combine(basePath, fileName);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }
    }
}
