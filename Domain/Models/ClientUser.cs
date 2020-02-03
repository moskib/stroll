using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class ClientUser
    {
        [Key]
        public Guid UserId { get; set; }
        [RegularExpression("[a-zA-Z]")]
        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }
        [RegularExpression("[a-zA-Z]")]
        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }
        [Required, Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
