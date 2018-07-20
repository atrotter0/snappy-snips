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
    public class SpecialtiesController : Controller
    {
        private HairSalonContext db = new HairSalonContext();

        [HttpGet("/specialties")]
        public ActionResult Index()
        {
            return View(db.Specialties.ToList());
        }

        [HttpGet("/specialties/new")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("/specialties/new")]
        public ActionResult Create(Specialty specialty)
        {
            db.Specialties.Add(specialty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/specialties/{id}")]
        public ActionResult Show(int id)
        {
            Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
            //ViewBag.ClientList = db.Clients.Where(client => client.SpecialtyId == id).ToList();
            return View(specialty);
        }

        [HttpGet("/specialties/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
            return View(specialty);
        }

        [HttpPost("/specialties/{id}/edit")]
        public ActionResult Edit(int id, Specialty specialty)
        {
            db.Entry(specialty).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/specialties/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
            db.Specialties.Remove(specialty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
