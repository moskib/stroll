using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class BusinessUser
    {
        [Key]
        public Guid BusinessId { get; set; }
        [Key]
        public Guid UserID { get; set; }
        [RegularExpression("[a-zA-Z]")]
        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        [RegularExpression("[a-zA-Z]")]
        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }
        [Phone(ErrorMessage ="Invalid phone number")]
        public string Phone { get; set; }

        // Navigation properties
        [ForeignKey(nameof(BusinessId)]
        public Business Business { get; set; }
        [ForeignKey(nameof(UserID)]
        public User User { get; set; }
    }
}
