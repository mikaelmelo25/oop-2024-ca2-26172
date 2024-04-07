//Name:  Mikael Melo
//student number: 26172

using System.ComponentModel.DataAnnotations;

namespace BankingMVC.Data.Entities
{
    public class SavingsAccountTransactionEntity
    {
        [Key]
        public int Id { get; set; }

        public int SavingsAccountId { get; set; }
        public SavingsAccountEntity SavingsAccount { get; set; }

        public int TransactionId { get; set; }
        public TransactionEntity Transaction { get; set; }
    }
}