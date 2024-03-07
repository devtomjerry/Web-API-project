// UserRegistrationDto.cs
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,12}$", ErrorMessage = "The mobile number must be up to 12 digits.")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
