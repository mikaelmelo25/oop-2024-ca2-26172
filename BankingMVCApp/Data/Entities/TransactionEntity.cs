//Name:  Mikael Melo
//student number: 26172

using BankingSharedLibrary.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankingMVC.Data.Entities
{
    public class TransactionEntity : Transaction
    {
        [Key]
        public int Id { get; set; }

      
        public TransactionEntity(DateTime date, string action, decimal amount, decimal finalBalance)
            : base(date, action, amount, finalBalance)
        {
        }
    }
}