﻿// UserRegistrationDto.cs
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
