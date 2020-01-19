using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroll.Models
{
    public class ClientUser
    {
        public Guid UID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
