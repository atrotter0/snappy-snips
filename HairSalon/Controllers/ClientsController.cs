using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        private HairSalonContext db = new HairSalonContext();

        [HttpGet("/clients")]
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        [HttpGet("/clients/new")]
        public ActionResult Create()
        {
            ViewBag.StylistId = db.Stylists.ToList()
                .Select(stylist => new SelectListItem {
                    Value = stylist.StylistId.ToString(),
                    Text = stylist.FirstName + " " + stylist.LastName
                })
                .ToList();
            return View();
        }

        [HttpPost("/clients/new")]
        public ActionResult Create(Client client)
        {
            db.Clients.Add(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/clients/{id}")]
        public ActionResult Show(int id)
        {
            Client client = db.Clients.FirstOrDefault(item => item.ClientId == id);
            return View(client);
        }

        [HttpGet("/clients/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Client client = db.Clients.FirstOrDefault(item => item.ClientId == id);
            return View(client);
        }

        [HttpPost("/clients/{id}/edit")]
        public ActionResult Edit(int id, Client client)
        {
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/clients/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Client client = db.Clients.FirstOrDefault(item => item.ClientId == id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
