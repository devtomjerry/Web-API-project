﻿using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public string Address { get; set; }

        public string MobileNumber { get; set; }
        public string PasswordHash { get; set; }

    }
}
