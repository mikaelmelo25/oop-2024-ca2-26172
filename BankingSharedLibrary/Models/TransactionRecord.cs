//Name:  Mikael Melo
//student number: 26172

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSharedLibrary.Models
{
    public class TransactionRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
    }
}