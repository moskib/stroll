using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class Schedule
    {
        public Guid UID { get; set; }
        public Guid BusinessUserID { get; set; }
        public Guid ClientUserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AppointmentStatus { get; set; }
    }
}
