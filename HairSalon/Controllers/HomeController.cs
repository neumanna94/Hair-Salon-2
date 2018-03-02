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
            ViewBag.Specialities = Speciality.GetAll();
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
            string specialityId = Request.Form["specialityId"];

            Stylist newStylist = new Stylist(name, 0);
            newStylist.Save();

            Speciality newSpeciality = new Speciality();
            newSpeciality.Stylist_Specialities_Save(newStylist.GetId(),Int32.Parse(specialityId));

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
            Speciality newSpeciality = new Speciality();
            ViewBag.Specialities = newSpeciality.SpecialitiesOfStylist(id);
            ViewBag.AllSpecialities = Speciality.GetAll();
            return View(Stylist.Find(id));
        }

        [HttpPost("/stylists/{id}")]
        public ActionResult AddStyletoStylist(int id)
        {
            string specialityId = Request.Form["specialityId"];

            Speciality newSpeciality = new Speciality();
            newSpeciality.Stylist_Specialities_Save(id,Int32.Parse(specialityId));

            ViewBag.Specialities = newSpeciality.SpecialitiesOfStylist(id);
            ViewBag.AllSpecialities = Speciality.GetAll();
            return RedirectToAction("StylistDetail");
        }
        [HttpPost("/stylists/new/name/{id}")]
        public ActionResult ChangeStylistName(int id)
        {
            string newStylistName = Request.Form["nameInput"];
            Stylist newStylist = new Stylist(id);
            newStylist.UpdateName(newStylistName);


            return RedirectToAction("StylistDetail");
        }
    }
}
