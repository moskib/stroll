using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class Business
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
