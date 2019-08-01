using System;
using System.Collections.Generic;

namespace API.Entities
{
    public partial class Dayslots
    {
        public Dayslots()
        {
            Meetings = new HashSet<Meetings>();
        }

        public int IdDay { get; set; }
        public DateTime Day { get; set; }

        public virtual ICollection<Meetings> Meetings { get; set; }
    }
}
