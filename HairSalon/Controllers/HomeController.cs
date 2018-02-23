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
            string name = Request.Form["name"];
            Stylist newStylist = new Stylist(name, 0);
            newStylist.Save();
            return RedirectToAction("newStylist");
        }

        [HttpPost("/new/client")]
        public ActionResult newClientPost()
        {
            string name = Request.Form["name"];
            string stylist = Request.Form["stylist"];
            Client newClient = new Client(name, Int32.Parse(stylist), 0);
            newClient.Save();
            return RedirectToAction("newClient");
        }

        [HttpGet("/stylists")]
        public ActionResult Stylists(int id)
        {
            return View("allStylists", Stylist.GetAll());
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult StylistDetail(int id)
        {
            return View(Stylist.Find(id));
        }

    }
}
