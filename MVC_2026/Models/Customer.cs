using System.ComponentModel.DataAnnotations;

namespace MVC2026.Models
{
    public class Customer
    {
        [Key]
        public int CustmId { get; set; }

        public string Custm_name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string PlainTextPassword { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }

        public DateTime Createddate { get; set; }
    }
}
