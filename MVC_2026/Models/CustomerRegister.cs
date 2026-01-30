using System.ComponentModel.DataAnnotations;

namespace MVC2026.Models
{
    public class CustomerRegister
    {
        [Required]
        public string Custm_name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits")]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}
