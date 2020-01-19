using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class Schedule
    {
        [Key]
        public Guid UID { get; set; }
        [Required]
        public Guid BusinessUserID { get; set; }
        [Required]
        public Guid ClientUserID { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int Status { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ClientUserID))]
        public ClientUser ClientUser { get; set; }

        [ForeignKey(nameof(BusinessUserID))]
        public BusinessUser BusinessUser { get; set; }

        [ForeignKey(nameof(Status))]
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
