using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private HttpClient _client;
        public HomeController()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:2019")
            };
        }

        [Route("")]
        [Route("[action]")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("[action]")]
        public ActionResult Create()
        {
            string dayResult = _client.GetStringAsync("days/days").Result;
            List<ModelDayslot> dayList = JsonConvert.DeserializeObject<List<ModelDayslot>>(dayResult);

            string timeslotResult = _client.GetStringAsync("timeslots/timeslots").Result;
            List<ModelTimeslot> timeslotList = JsonConvert.DeserializeObject<List<ModelTimeslot>>(timeslotResult);

            ViewBag.days = dayList;
            ViewBag.timeslots = timeslotList;

            return View(ViewBag);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AddMeetingAsync()
        {
            int day = Int32.Parse(Request.Form["day"]);
            int timeslot = Int32.Parse(Request.Form["timeslot"]);

            ModelMeeting newMeeting = new ModelMeeting()
            {
                IdDay = day,
                IdTimeslot = timeslot
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync
                ("Meetings/AddMeeting", newMeeting);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ShowListOfMeetings", "Admin");
        }
    }
}
