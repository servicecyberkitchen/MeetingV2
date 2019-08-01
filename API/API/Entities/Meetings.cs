using System;
using System.Collections.Generic;

namespace API.Entities
{
    public partial class Meetings
    {
        public int IdMeeting { get; set; }
        public int IdDay { get; set; }
        public int IdTimeslot { get; set; }
        public int IdStatus { get; set; }

        public virtual Dayslots IdDayNavigation { get; set; }
        public virtual Meetingstatus IdStatusNavigation { get; set; }
        public virtual Timeslots IdTimeslotNavigation { get; set; }
    }
}
