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
            ViewBag.AllClients = Client.FindAllClientByStylistId(id);
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
        [HttpGet("/client/display/{id}")]
        public ActionResult ClientDetail(int id)
        {
            Client newClient = new Client();
            newClient = Client.Find(id);

            ViewBag.ClientStylist = Stylist.Find(newClient.GetStylistId());
            ViewBag.AllStylists = Stylist.GetAll();

            return View(newClient);
        }
        [HttpPost("/client/display/update/{id}")]
        public ActionResult ClientDetailUpdate(int id)
        {
            string name = Request.Form["nameInput"];
            int stylistId = Int32.Parse(Request.Form["stylistId"]);
            Client newClient = new Client(id);
            newClient.Update(name, stylistId);

            return RedirectToAction("ClientDetail");
        }

        [HttpGet("/client/display/delete/{id}")]
        public ActionResult ClientDelete(int id)
        {
            Client.DeleteRow(id);
            return RedirectToAction("Stylists");
        }

        [HttpGet("/client/display/delete/all")]
        public ActionResult ClientDeleteAll()
        {
            Client.DeleteAll();
            return RedirectToAction("Stylists");
        }

        [HttpGet("/stylist/display/delete/{id}")]
        public ActionResult StylistDelete(int id)
        {
            Stylist.DeleteRow(id);
            return RedirectToAction("Stylists");
        }

        [HttpGet("/stylist/display/delete/all")]
        public ActionResult StylistDeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Stylists");
        }
    }
}
