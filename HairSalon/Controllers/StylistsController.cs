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
    public class StylistsController : Controller
    {
        private HairSalonContext db = new HairSalonContext();

        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            return View(db.Stylists.ToList());
        }

        [HttpGet("/stylists/new")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("/stylists/new")]
        public ActionResult Create(Stylist stylist)
        {
            db.Stylists.Add(stylist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Details(int id)
        {
            Stylist stylist = db.Stylists.FirstOrDefault(item => item.StylistId == id);
            return View(stylist);
        }

        [HttpGet("/stylists/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Stylist stylist = db.Stylists.FirstOrDefault(item => item.StylistId == id);
            return View(stylist);
        }

        [HttpPost("/stylists/{id}/edit")]
        public ActionResult Edit(int id, Stylist stylist)
        {
            db.Entry(stylist).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
