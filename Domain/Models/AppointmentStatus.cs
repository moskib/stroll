using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class AppointmentStatus
    {
        [Key]
        public int UID { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string Status { get; set; }
    }
}
