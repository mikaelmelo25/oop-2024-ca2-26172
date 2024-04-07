
//Name:  Mikael Melo
//student number: 26172

using System;
using System.Collections.Generic;
using BankingSharedLibrary.Models;

namespace BankingSharedLibrary.Models
{
    public class BankEmployee : User
    {
        public BankEmployee(string firstName, string lastName, string accountNumber, string pin)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountNumber = accountNumber;
            Pin = pin;
        }

        public Customer CreateCustomer(string firstName, string lastName, string pin)
        {
            // Logic to create a new customer
            int customerId = GetNextCustomerId(); 
            Customer newCustomer = new Customer(customerId, firstName, lastName, GenerateAccountNumber(firstName, lastName), pin);

           

            return newCustomer;
        }

        public bool DeleteCustomer(Customer customer)
        {
            //  delete a customer
            if (customer.SavingsAccount.Balance == 0 && customer.CurrentAccount.Balance == 0)
            {
             
                return true;
            }
            else
            {
                return false;
            }
        }

        public Transaction CreateTransaction(Customer customer, string accountType, string action, decimal amount)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }

            // Logic to create a transaction
            Transaction transaction = new Transaction(DateTime.Now, action, amount, 0); 

            if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                if (customer.SavingsAccount == null)
                {
                    throw new NullReferenceException("SavingsAccount property of the customer is null.");
                }

                customer.SavingsAccount.AddMoney(amount);
                transaction.FinalBalance = customer.SavingsAccount.Balance;
            }
            else if (accountType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                if (customer.CurrentAccount == null)
                {
                    throw new NullReferenceException("CurrentAccount property of the customer is null.");
                }

                customer.CurrentAccount.AddMoney(amount);
                transaction.FinalBalance = customer.CurrentAccount.Balance;
            }
            else
            {
                throw new ArgumentException("Invalid account type.", nameof(accountType));
            }

           
            customer.SavingsAccount.CreateTransactionRecord(transaction.Date, transaction.Action, transaction.Amount, transaction.FinalBalance);

            return transaction;
        }

        //  generate a unique account number
        public string GenerateAccountNumber(string firstName, string lastName)
        {
            string initials = $"{firstName.Substring(0, 1)}{lastName.Substring(0, 1)}".ToLower();
            int nameLength = firstName.Length + lastName.Length;
            int firstInitialPosition = firstName.ToLower()[0] - 'a' + 1;
            int secondInitialPosition = lastName.ToLower()[0] - 'a' + 1;
            string pin = $"{firstInitialPosition:D2}{secondInitialPosition:D2}";
            return $"{initials}-{nameLength:D2}-{pin}";
        }

        // Method to generate a PIN based on the customer's first and last name
        public string GeneratePin(string firstName, string lastName)
        {
            string pin = $"{firstName.Substring(0, 2)}{lastName.Substring(0, 2)}";
            return pin;
        }

        private int GetNextCustomerId()
        {
           
            return 1; 
        }
    }
}