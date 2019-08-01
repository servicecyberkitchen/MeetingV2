using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DaysController : Controller
    {
        AppointmentsContext db;
        public DaysController()
        {
            db = new AppointmentsContext();
        }

        [Route("Days")]
        // GET Days/Days
        public ActionResult<IEnumerable<ModelDayslot>> ListDays()
        {
            try
            {
                IEnumerable<ModelDayslot> listDays = db.Dayslots
                    .Select(d => new ModelDayslot
                    {
                        IdDay = d.IdDay,
                        Day = d.Day
                    })
                    .ToList();

                if (listDays == null || listDays.Count() == 0)
                    return NotFound("No days were found.");

                return Ok(listDays);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Route("Days/{id}")]
        // GET Days/Days/1
        public IActionResult getDayById(int id)
        {
            var dayById = db.Dayslots
                .Where(c => c.IdDay == id)
                  .Select(d => new ModelDayslot
                  {
                      IdDay = d.IdDay,
                      Day = d.Day
                  }).ToList();
            return Ok(dayById);
        }
    }
}