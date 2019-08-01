using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        AppointmentsContext db;
        public MeetingsController()
        {
            db = new AppointmentsContext();
        }

        [Route("Meetings")]
        // GET Meetings/Meetings
        public IActionResult ListMeetings()
        {
            var listMeetings =
                from a in db.Meetings
                join b in db.Dayslots
                    on a.IdDay equals b.IdDay
                join c in db.Timeslots
                    on a.IdTimeslot equals c.IdTimeslot
                join d in db.Meetingstatus
                    on a.IdStatus equals d.IdMeSt
                select new ModelMeetingFinal
                {
                    IdMeeting = a.IdMeeting,
                    Day = b.Day,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    Status = d.Status
                };
            return Ok(listMeetings);
        }

        //[Route("Meetings/{id}")]
        [HttpGet("{id}", Name = "GetMeeting")]
        // GET Meetings/Meetings/1
        public IActionResult getMeetingById(int id)
        {
            var meetingById = db.Meetings
                .Where(c => c.IdMeeting == id)
                  .Select(d => new ModelMeeting
                  {
                      IdMeeting = d.IdMeeting,
                      IdDay = d.IdDay,
                      IdTimeslot = d.IdTimeslot,
                      IdStatus = d.IdStatus                     
                  })
                  .ToList();
            return Ok(meetingById);
        }


       

        #region POST methods (creating new resources)
        [HttpPost]
        [Route("AddMeeting")]
        public IActionResult AddMeeting([FromBody] ModelMeeting content)
        {
            int statusPending = 1;

            Meetings newMeeting = new Meetings
            {
                IdDay = content.IdDay,
                IdTimeslot = content.IdTimeslot,
                IdStatus = statusPending
            };

            db.Meetings.Add(newMeeting);
            db.SaveChanges();
            return Created(Url.Link("GetMeeting", new { id = newMeeting.IdMeeting }), newMeeting);
        }

        [HttpPost]
        [Route("UpdateMeetingStatus")]
        public IActionResult UpdateMeeting([FromBody] ModelMeeting content)
        {
            Meetings meetingById = db.Meetings
               .Where(c => c.IdMeeting == content.IdMeeting)
               .FirstOrDefault();

            meetingById.IdStatus = content.IdStatus;

            db.Update(meetingById);
            db.SaveChanges();
            return Created(Url.Link("GetMeeting", new { id = meetingById.IdMeeting }), meetingById);
        }
        #endregion






        //[Route("Meetings/{id}")]
        [HttpGet("{id}/{status}", Name = "updateMeetingsStatus")]
        // GET Meetings/1/1
        public IActionResult updateMeetingsStatus(int id, int status)
        {
            Meetings meetingById = db.Meetings
                .Where(c => c.IdMeeting == id)
                .FirstOrDefault();

            meetingById.IdStatus = status;
            db.Update(meetingById);
            db.SaveChanges();

            return Ok(meetingById);
        }
    }
}