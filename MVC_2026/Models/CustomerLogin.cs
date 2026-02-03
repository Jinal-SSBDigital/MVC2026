//namespace MVC2026.Models
//{
//    public class CustomerLogin
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace MVC2026.Models
{
    public class CustomerLogin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }
}