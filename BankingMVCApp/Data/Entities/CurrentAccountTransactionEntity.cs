//Name:  Mikael Melo
//student number: 26172

using BankingMVC.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class CurrentAccountTransactionEntity
{
    [Key]
    public int Id { get; set; }

    public int CurrentAccountId { get; set; }
    [ForeignKey("CurrentAccountId")]
    public CurrentAccountEntity CurrentAccount { get; set; }

    public int TransactionId { get; set; }
    [ForeignKey("TransactionId")]
    public TransactionEntity Transaction { get; set; }
}