//Name:  Mikael Melo
//student number: 26172

namespace BankingSharedLibrary.Models
{
    public abstract class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AccountNumber { get; set; }
        public string? Pin { get; set; }
    }
}
