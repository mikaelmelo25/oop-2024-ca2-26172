//Name:  Mikael Melo
//student number: 26172

using System;
using System.Collections.Generic;
using BankingSharedLibrary.Models;

namespace BankingSharedLibrary.Services
{
    public class LoginService
    {
        public bool LoginAsEmployee(string pin)
        {
            
            return pin == "A1234"; //  "A1234" is the correct PIN for bank employees
        }

        public bool LoginAsCustomer(Customer customer, string firstName, string lastName, string accountNumber, string pin)
        {
            // Validate customer credentials
            return customer.FirstName == firstName &&
                   customer.LastName == lastName &&
                   customer.AccountNumber == accountNumber &&
                   customer.Pin == pin;
        }
    }
}
