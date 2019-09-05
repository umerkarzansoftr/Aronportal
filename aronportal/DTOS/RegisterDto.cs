﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aronportal.DTOS
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be at least 3 characters")]
        public string FullName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be at least 3 characters")]
        public string Email { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "You must provide password between 8 and 20 characters")]
        public string Password { get; set; }
    }
}
