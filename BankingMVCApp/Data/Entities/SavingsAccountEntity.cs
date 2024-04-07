//Name:  Mikael Melo
//student number: 26172

using BankingMVC.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using BankingSharedLibrary.Models;

public class SavingsAccountEntity
{
    public SavingsAccountEntity(string accountNumber, string pin)
    {
        AccountNumber = accountNumber;
        Pin = pin;
        Customer = new CustomerEntity ();
        Transactions = new List<SavingsAccountTransactionEntity>();
    }

    [Key]
    public int Id { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    public CustomerEntity Customer { get; set; }

    public string AccountNumber { get; set; }

    public string Pin { get; set; }

    public decimal Balance { get; set; }

    public ICollection<SavingsAccountTransactionEntity> Transactions { get; set; }
}