using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ModelMeeting
    {
        public int IdMeeting { get; set; }
        public int IdDay { get; set; }
        public int IdTimeslot { get; set; }
        public int IdStatus { get; set; }
    }
}
