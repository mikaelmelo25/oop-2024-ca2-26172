//Name:  Mikael Melo
//student number: 26172

using BankingSharedLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingMVC.Data.Entities
{
    public class TransactionRecordEntity : TransactionRecord
    {
        [Key]
        public new int Id { get; set; }

        public int SavingsAccountId { get; set; }
        public SavingsAccountEntity? SavingsAccount { get; set; }

        public int CurrentAccountId { get; set; }
        public CurrentAccountEntity? CurrentAccount { get; set; }

        public new int TransactionId { get; set; }
        public new TransactionEntity? Transaction { get; set; }
    }
}