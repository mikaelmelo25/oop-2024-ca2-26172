//Name:  Mikael Melo
//student number: 26172

using BankingMVC.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class CurrentAccountEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }

    public string? AccountNumber { get; set; }
    public string? Pin { get; set; }
    public decimal Balance { get; set; }

    public ICollection<CurrentAccountTransactionEntity>? Transactions { get; set; }
}