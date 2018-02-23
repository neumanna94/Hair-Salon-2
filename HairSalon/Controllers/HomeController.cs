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

        [HttpGet("/new/client/{id}")]
        public ActionResult newClient(int id)
        {

            return View("newClient", id);
        }
        [HttpPost("/new/client/{id}")]
        public ActionResult newClientPost(int id)
        {
            string name = Request.Form["name"];
            Client newClient = new Client(name, 0, id);
            newClient.Save();
            return RedirectToAction("StylistDetail", id);
        }

        [HttpPost("/new/stylist")]
        public ActionResult newStylistPost()
        {
            string name = Request.Form["name"];
            Stylist newStylist = new Stylist(name, 0);
            newStylist.Save();
            return RedirectToAction("newStylist");
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
