﻿using System.ComponentModel.DataAnnotations;

namespace NBK_Client.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
