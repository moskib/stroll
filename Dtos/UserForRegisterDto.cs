﻿using Stroll.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stroll.Dtos
{
    public class UserForRegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength
            (
                20,
                MinimumLength =8,
                ErrorMessage ="Password length must be between 8-20 characters"
            )
        ]
        public string Password { get; set; }
        [Required]
        public int Type { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }
    }
}
