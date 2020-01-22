using System;
using System.ComponentModel.DataAnnotations;

namespace Stroll.Dtos
{
    public class UserForLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
