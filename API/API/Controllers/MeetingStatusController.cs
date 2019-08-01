using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MeetingStatusController : Controller
    {
        AppointmentsContext db;
        public MeetingStatusController()
        {
            db = new AppointmentsContext();
        }

        [Route("Meetingstatus")]
        // GET Meetingstatus/Meetingstatus
        public IActionResult ListMeetingstatus()
        {
            var listMeetingstatus = db.Meetingstatus
                .Select(c => new ModelMeetingstatus {
                    IdMeSt = c.IdMeSt,
                    Status = c.Status
                })
                .ToList();
            return Ok(listMeetingstatus);
        }

        [HttpGet("{id}", Name = "getMeetingstatus")]
        // GET Meetingstatus/1
        public IActionResult getMeetingstatusById(int id)
        {
            var meetingstatusById = db.Meetingstatus
                .Where(c => c.IdMeSt == id)
                .Select(c => new ModelMeetingstatus
                {
                    IdMeSt = c.IdMeSt,
                    Status = c.Status
                })
                .ToList();
            return Ok(meetingstatusById);
        }
    }
}