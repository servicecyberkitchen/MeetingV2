using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private HttpClient _client;
        public AdminController()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:2019")
            };
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("Meetings")]
        public IActionResult ShowListOfMeetings()
        {
            string meetingsResult = _client.GetStringAsync("Meetings/Meetings").Result;
            List<ModelMeetingFinal> meetingsList = JsonConvert.DeserializeObject<List<ModelMeetingFinal>>(meetingsResult);

            ViewBag.meetings = meetingsList;
            return View("Meetings", ViewBag);
        }

        [Route("[action]")]
        public async Task<ActionResult> UpdateMeetingStatus(int id, int status) {

            ModelMeeting newMeeting = new ModelMeeting()
            {
                IdMeeting = id,
                IdStatus = status
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync
            ("Meetings/UpdateMeetingStatus", newMeeting);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ShowListOfMeetings");
        }
    }
}