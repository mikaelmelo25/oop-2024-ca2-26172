//Name:  Mikael Melo
//student number: 26172

using BankingSharedLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace BankingMVC.Data.Entities
{
    public class CustomerEntity : Customer
    {
        [Key]
        public new int Id { get; set; }
        public new SavingsAccountEntity? SavingsAccount { get; set; }
        public new CurrentAccountEntity? CurrentAccount { get; set; }

        public CustomerEntity(int id, string firstName, string lastName, string accountNumber, string pin)
            : base(id, firstName, lastName, accountNumber, pin)
        {
        }
    }
}