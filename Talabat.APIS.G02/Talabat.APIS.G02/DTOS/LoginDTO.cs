using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.G02.DTOS
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
