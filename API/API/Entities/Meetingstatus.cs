using System;
using System.Collections.Generic;

namespace API.Entities
{
    public partial class Meetingstatus
    {
        public Meetingstatus()
        {
            Meetings = new HashSet<Meetings>();
        }

        public int IdMeSt { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Meetings> Meetings { get; set; }
    }
}
