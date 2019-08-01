using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimeslotsController : ControllerBase
    {
        AppointmentsContext db;
        public TimeslotsController()
        {
            db = new AppointmentsContext();
        }

        [Route("Timeslots")]
        // GET Timeslots/Timeslots
        public IActionResult ListTimeslots()
        {
            var listTimeslots = db.Timeslots
                .Select(c => c)
                .ToList();
            return Ok(listTimeslots);
        }

        [Route("Timeslots/{id}")]
        // GET Timeslots/Timeslots/1
        public IActionResult getTimeslotById(int id)
        {
            var timeslotById = db.Timeslots
                .Where(c => c.IdTimeslot == id)
                .Select(c => c);
            return Ok(timeslotById);
        }
    }
}