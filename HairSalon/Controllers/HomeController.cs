using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet("/new/stylist")]
        public ActionResult newStylist()
        {
            return View("newStylist");
        }
        [HttpGet("/new/client")]
        public ActionResult newClient()
        {
            return View("newClient");
        }
        [HttpPost("/new/stylist")]
        public ActionResult newStylistPost()
        {
            return View("allStylists", Stylist.GetAll());
        }
        [HttpPost("/new/client")]
        public ActionResult newClientPost()
        {
            return View("allStylists", Stylist.GetAll());
        }
        [HttpGet("/stylists")]
        public ActionResult Stylists(int id)
        {
            return View("allStylists", Stylist.GetAll());
        }
        [HttpGet("/stylists/{id}")]
        public ActionResult StylistDetails(int id)
        {
            return View(Stylist.Find(id));
        }

    }
}
