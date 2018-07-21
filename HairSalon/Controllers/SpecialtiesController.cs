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
            ViewBag.StylistIds = db.Stylists.ToList()
                .Select(stylist => new SelectListItem {
                    Value = stylist.StylistId.ToString(),
                    Text = stylist.FirstName + " " + stylist.LastName
                })
                .ToList();
            return View();
        }

        [HttpPost("/specialties/new")]
        public ActionResult Create(Specialty specialty, List<int> StylistIds)
        {
            db.Specialties.Add(specialty);
            foreach (int stylistId in StylistIds)
            {
                StylistSpecialty newStylistSpecialty = new StylistSpecialty(stylistId, specialty.SpecialtyId);
                db.StylistsSpecialties.Add(newStylistSpecialty);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/specialties/{id}")]
        public ActionResult Show(int id)
        {
            Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
            var entryList = db.StylistsSpecialties.Where(entry => entry.SpecialtyId == id).ToList();
            List<Stylist> stylistList = new List<Stylist>();
            foreach (var stylist in entryList)
            {
                int stylistId = stylist.StylistId;
                stylistList.Add(db.Stylists.FirstOrDefault(record => record.StylistId == stylistId));
            }
            ViewBag.StylistList = stylistList;
            return View(specialty);
        }

        [HttpGet("/specialties/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
            ViewBag.StylistIds = db.Stylists.ToList()
                .Select(stylist => new SelectListItem {
                    Value = stylist.StylistId.ToString(),
                    Text = stylist.FirstName + " " + stylist.LastName
                })
                .ToList();
            return View(specialty);
        }

        [HttpPost("/specialties/{id}/edit")]
        public ActionResult Edit(Specialty specialty, List<int> StylistIds)
        {
            db.Entry(specialty).State = EntityState.Modified;
            var specialtyMatchesInJoinTable = db.StylistsSpecialties.Where(entry => entry.SpecialtyId == specialty.SpecialtyId).ToList();
            foreach (var stylist in specialtyMatchesInJoinTable)
            {
                int stylistId = stylist.StylistId;
                var joinEntry = db.StylistsSpecialties
                                  .Where(entry => entry.StylistId == stylistId)
                                  .Where(entry => entry.SpecialtyId == specialty.SpecialtyId);
                foreach (var entry in joinEntry)
                {
                    db.StylistsSpecialties.Remove(entry);
                }
            }
            foreach (var id in StylistIds)
            {
                Stylist stylist = db.Stylists.FirstOrDefault(_ => _.StylistId == id);
                StylistSpecialty newStylistSpecialty = new StylistSpecialty(stylist.StylistId, specialty.SpecialtyId);
                db.StylistsSpecialties.Add(newStylistSpecialty);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/specialties/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
            StylistSpecialty joinEntry = db.StylistsSpecialties.FirstOrDefault(entry => entry.SpecialtyId == id);
            db.Specialties.Remove(specialty);
            if (joinEntry != null) db.StylistsSpecialties.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
