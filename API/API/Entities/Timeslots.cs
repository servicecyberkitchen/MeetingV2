using System;
using System.Collections.Generic;

namespace API.Entities
{
    public partial class Timeslots
    {
        public Timeslots()
        {
            Meetings = new HashSet<Meetings>();
        }

        public int IdTimeslot { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Meetings> Meetings { get; set; }
    }
}
