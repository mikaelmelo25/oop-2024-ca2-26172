//Name:  Mikael Melo
//student number: 26172

using System;
using System.Collections.Generic;
using System.IO;
using BankingSharedLibrary.Models;
using BankingSharedLibrary.Services;

namespace BankingConsoleApp.Console
{
    public class ConsoleBank
    {
        private List<Customer> customers;
        private LoginService loginService;
        private BankEmployee bankEmployee;
        private Customer currentCustomer; 

        // Constructor
        public ConsoleBank()
        {
            customers = new List<Customer>();
            loginService = new LoginService();
            LoadCustomersFromFile(); // Load existing customers 
            bankEmployee = new BankEmployee("John", "Doe", "1234567890", "password"); 
        }

        // Load existing customers from file
        private void LoadCustomersFromFile()
        {
            try
            {
                // Read customers from file 
                if (File.Exists("customers.txt"))
                {
                    string[] lines = File.ReadAllLines("customers.txt");
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            string firstName = parts[0];
                            string lastName = parts[1];
                            string accountNumber = parts[2];
                            string pin = parts[3];
                            customers.Add(new Customer(firstName, lastName, accountNumber, pin));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error loading customers from file: " + ex.Message);
            }
        }

        // Save customers to file
        private void SaveCustomersToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("customers.txt"))
                {
                    foreach (Customer customer in customers)
                    {
                        writer.WriteLine($"{customer.FirstName},{customer.LastName},{customer.AccountNumber},{customer.Pin}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error saving customers to file: " + ex.Message);
            }
        }

        // Main menu for the console application
        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                System.Console.WriteLine("Welcome to the Banking Console App!");
                System.Console.WriteLine("1. Bank Employee Login");
                System.Console.WriteLine("2. Customer Login");
                System.Console.WriteLine("3. Exit");
                System.Console.Write("Enter your choice: ");
                string choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BankEmployeeLogin();
                        break;
                    case "2":
                        if (CustomerLogin()) // proceed if customer login is successful
                        {
                            CustomerMenu(); 
                        }
                        break;
                    case "3":
                        exit = true; 
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Bank employee login
        private void BankEmployeeLogin()
        {
            System.Console.Write("Enter PIN: ");
            string pin = System.Console.ReadLine();
            if (loginService.LoginAsEmployee(pin))
            {
                // Successful login
                BankEmployeeMenu();
            }
            else
            {
                System.Console.WriteLine("Invalid PIN. Please try again.");
            }
        }

        // Bank employee menu
        private void BankEmployeeMenu()
        {
            bool logout = false;
            while (!logout)
            {
                System.Console.WriteLine("Bank Employee Menu:");
                System.Console.WriteLine("1. Create Customer");
                System.Console.WriteLine("2. Delete Customer");
                System.Console.WriteLine("3. Create Transaction");
                System.Console.WriteLine("4. Show Customer List");
                System.Console.WriteLine("5. Logout");
                System.Console.Write("Enter your choice: ");
                string choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCustomer();
                        break;
                    case "2":
                        DeleteCustomer();
                        break;
                    case "3":
                        CreateTransaction();
                        break;
                    case "4":
                        ShowCustomerList();
                        break;
                    case "5":
                        logout = true;
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // create  new customer
        private void CreateCustomer()
        {
            System.Console.Write("Enter customer first name: ");
            string firstName = System.Console.ReadLine();
            System.Console.Write("Enter customer last name: ");
            string lastName = System.Console.ReadLine();
            System.Console.Write("Enter customer PIN: ");
            string pin = System.Console.ReadLine();

            // Generate a unique account number
            string accountNumber = GenerateAccountNumber(firstName, lastName);

            // Create a new customer object
            Customer newCustomer = bankEmployee.CreateCustomer(firstName, lastName, pin);

            // Add the new customer to the list
            customers.Add(newCustomer);

            System.Console.WriteLine("Customer created successfully.");

            // Save the updated list of customers to file
            SaveCustomersToFile();
        }

        // delete a customer
        private void DeleteCustomer()
        {
            try
            {
                System.Console.Write("Enter customer account number to delete: ");
                string accountNumber = System.Console.ReadLine();

                // Find the customer by account number
                Customer customerToDelete = customers.Find(c => c.AccountNumber == accountNumber);

                if (customerToDelete != null)
                {
                    // Check if customer's account balances are zero before deletion
                    if (bankEmployee.DeleteCustomer(customerToDelete))
                    {
                        customers.Remove(customerToDelete);
                        SaveCustomersToFile();
                        System.Console.WriteLine("Customer deleted successfully.");
                    }
                    else
                    {
                        System.Console.WriteLine("Cannot delete customer. Account balances are not zero.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deleting customer: " + ex.Message);
            }
        }

        // Method to create a transaction
        private void CreateTransaction()
        {
            try
            {
                System.Console.WriteLine("Enter customer account number: ");
                string accountNumber = System.Console.ReadLine();

                // Find the customer by account number
                Customer customer = customers.Find(c => c.AccountNumber == accountNumber);

                if (customer != null)
                {
                    System.Console.WriteLine("Enter transaction type (Deposit/Withdraw): ");
                    string action = System.Console.ReadLine();

                    System.Console.WriteLine("Enter transaction amount: ");
                    decimal amount = Convert.ToDecimal(System.Console.ReadLine());

                    System.Console.WriteLine("Enter account type (Savings/Current): ");
                    string accountType = System.Console.ReadLine();

                    // Perform the transaction using the bankEmployee object
                    Transaction transaction = bankEmployee.CreateTransaction(customer, accountType, action, amount);
                    System.Console.WriteLine("Transaction created successfully:");
                    System.Console.WriteLine($"Date: {transaction.Date}");
                    System.Console.WriteLine($"Action: {transaction.Action}");
                    System.Console.WriteLine($"Amount: {transaction.Amount}");
                    System.Console.WriteLine($"Final Balance: {transaction.FinalBalance}");
                }
                else
                {
                    System.Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error creating transaction: " + ex.Message);
            }
        }

        // Method to show the list of customers
        private void ShowCustomerList()
        {
            try
            {
                if (customers.Count > 0)
                {
                    System.Console.WriteLine("Customer List:");
                    foreach (var customer in customers)
                    {
                        System.Console.WriteLine($"First Name: {customer.FirstName}");
                        System.Console.WriteLine($"Last Name: {customer.LastName}");
                        System.Console.WriteLine($"Account Number: {customer.AccountNumber}");
                        System.Console.WriteLine($"PIN: {customer.Pin}");
                        System.Console.WriteLine("------------------");
                    }
                }
                else
                {
                    System.Console.WriteLine("No customers found.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error displaying customer list: " + ex.Message);
            }
        }

        // Customer login
        private bool CustomerLogin()
        {
            try
            {
                System.Console.Write("Enter your First Name: ");
                string firstName = System.Console.ReadLine();

                System.Console.Write("Enter your Last Name: ");
                string lastName = System.Console.ReadLine();

                System.Console.Write("Enter your Account Number: ");
                string accountNumber = System.Console.ReadLine();

                System.Console.Write("Enter your PIN: ");
                string pin = System.Console.ReadLine();

                // Find the customer with matching credentials
                Customer customer = customers.Find(c => c.FirstName == firstName && c.LastName == lastName && c.AccountNumber == accountNumber && c.Pin == pin);

                if (customer != null)
                {
                    System.Console.WriteLine("Login successful!");
                    currentCustomer = customer; // Store the current customer after login
                    return true;
                }
                else
                {
                    System.Console.WriteLine("Invalid credentials. Please try again.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error during customer login: " + ex.Message);
                return false;
            }
        }

        // Customer menu
        private void CustomerMenu()
        {
            bool logout = false;
            while (!logout)
            {
                System.Console.WriteLine("Customer Menu:");
                System.Console.WriteLine("1. Retrieve Transaction History");
                System.Console.WriteLine("2. Add Money");
                System.Console.WriteLine("3. Subtract Money");
                System.Console.WriteLine("4. Logout");
                System.Console.Write("Enter your choice: ");
                string choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RetrieveTransactionHistory();
                        break;
                    case "2":
                        AddMoney();
                        break;
                    case "3":
                        SubtractMoney();
                        break;
                    case "4":
                        logout = true;
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // retrieve transaction history
        private void RetrieveTransactionHistory()
        {
            try
            {
                System.Console.WriteLine("Enter account type (Savings/Current): ");
                string accountType = System.Console.ReadLine();

                // Retrieve transaction history based on account type
                List<Transaction> transactionHistory = currentCustomer.RetrieveTransactionHistory(accountType);

                // Display transaction history
                if (transactionHistory.Count > 0)
                {
                    System.Console.WriteLine($"Transaction History for {accountType} Account:");
                    foreach (var transaction in transactionHistory)
                    {
                        System.Console.WriteLine($"Date: {transaction.Date}");
                        System.Console.WriteLine($"Action: {transaction.Action}");
                        System.Console.WriteLine($"Amount: {transaction.Amount}");
                        System.Console.WriteLine($"Final Balance: {transaction.FinalBalance}");
                        System.Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    System.Console.WriteLine("No transactions found.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error retrieving transaction history: " + ex.Message);
            }
        }

        // Method to add money to account
        private void AddMoney()
        {
            try
            {
                System.Console.WriteLine("Enter account type (Savings/Current): ");
                string accountType = System.Console.ReadLine();

                System.Console.WriteLine("Enter amount to add: ");
                decimal amount = Convert.ToDecimal(System.Console.ReadLine());

                // Add money to the account
                currentCustomer.AddMoney(accountType, amount);

                System.Console.WriteLine("Money added successfully.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error adding money: " + ex.Message);
            }
        }

        // Method to subtract money from account
        private void SubtractMoney()
        {
            try
            {
                System.Console.WriteLine("Enter account type (Savings/Current): ");
                string accountType = System.Console.ReadLine();

                System.Console.WriteLine("Enter amount to subtract: ");
                decimal amount = Convert.ToDecimal(System.Console.ReadLine());

                // Subtract money from the account
                currentCustomer.SubtractMoney(accountType, amount);

                System.Console.WriteLine("Money subtracted successfully.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error subtracting money: " + ex.Message);
            }
        }

        // Helper method to generate a unique account number
        private string GenerateAccountNumber(string firstName, string lastName)
        {
            string initials = $"{firstName.Substring(0, 1)}{lastName.Substring(0, 1)}".ToLower();
            int nameLength = firstName.Length + lastName.Length;
            int firstInitialPosition = firstName.ToLower()[0] - 'a' + 1;
            int secondInitialPosition = lastName.ToLower()[0] - 'a' + 1;
            string pin = $"{firstInitialPosition:D2}{secondInitialPosition:D2}";
            return $"{initials}-{nameLength:D2}-{pin}";
        }
    }
}
