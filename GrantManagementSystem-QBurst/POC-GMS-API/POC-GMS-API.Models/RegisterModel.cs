using System.ComponentModel.DataAnnotations;

namespace POC_GMS_API.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; } //email
        [Required]
        public string Password { get; set; }
    }
}