﻿using System.ComponentModel.DataAnnotations;

namespace NBK_API.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
