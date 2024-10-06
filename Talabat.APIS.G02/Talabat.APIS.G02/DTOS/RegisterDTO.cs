using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.G02.DTOS
{
    public class RegisterDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
