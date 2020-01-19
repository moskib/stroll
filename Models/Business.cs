using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class Business
    {
        [Key]
        public Guid UID { get; set; }
        [RegularExpression("\\w")]
        [MaxLength(150), MinLength(2)]
        public string Name { get; set; }
        [MaxLength(300), MinLength(2)]
        public string Address { get; set; }
        [RegularExpression("[a-zA-Z]")]
        [MaxLength(50), MinLength(2)]
        public string City { get; set; }
        [Required, Phone(ErrorMessage ="Invalid phone number")]
        public string Phone { get; set; }
    }
}
