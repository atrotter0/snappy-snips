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
            ViewBag.SpecialtyIds = db.Specialties.ToList()
                .Select(specialty => new SelectListItem {
                    Value = specialty.SpecialtyId.ToString(),
                    Text = specialty.Name
                })
                .ToList();
            return View();
        }

        [HttpPost("/stylists/new")]
        public ActionResult Create(Stylist stylist, List<int> SpecialtyIds)
        {
            db.Stylists.Add(stylist);
            foreach (int specialtyId in SpecialtyIds)
            {
                StylistSpecialty newStylistSpecialty = new StylistSpecialty(stylist.StylistId, specialtyId);
                db.StylistsSpecialties.Add(newStylistSpecialty);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
            Stylist stylist = db.Stylists.FirstOrDefault(item => item.StylistId == id);
            ViewBag.ClientList = db.Clients.Where(client => client.StylistId == id).ToList();
            var entryList = db.StylistsSpecialties.Where(entry => entry.StylistId == id).ToList();
            List<Specialty> specialtyList = new List<Specialty>();
            foreach (var specialty in entryList)
            {
                int specialtyId = specialty.SpecialtyId;
                specialtyList.Add(db.Specialties.FirstOrDefault(record => record.SpecialtyId == specialtyId));
            }
            ViewBag.SpecialtyList = specialtyList;
            return View(stylist);
        }

        [HttpGet("/stylists/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Stylist stylist = db.Stylists.FirstOrDefault(item => item.StylistId == id);
            ViewBag.SpecialtyIds = db.Specialties.ToList()
                .Select(specialty => new SelectListItem {
                    Value = specialty.SpecialtyId.ToString(),
                    Text = specialty.Name
                })
                .ToList();
            return View(stylist);
        }

        [HttpPost("/stylists/{id}/edit")]
        public ActionResult Edit(Stylist stylist, List<int> SpecialtyIds)
        {
            db.Entry(stylist).State = EntityState.Modified;
            var matchesInJoinTable = db.StylistsSpecialties.Where(entry => entry.StylistId == stylist.StylistId).ToList();
            foreach (var specialty in matchesInJoinTable)
            {
                int specialtyId = specialty.SpecialtyId;
                var joinEntry = db.StylistsSpecialties
                                  .Where(entry => entry.SpecialtyId == specialtyId)
                                  .Where(entry => entry.StylistId == stylist.StylistId);
                foreach (var entry in joinEntry)
                {
                    db.StylistsSpecialties.Remove(entry);
                }
            }
            foreach (var id in SpecialtyIds)
            {
                Specialty specialty = db.Specialties.FirstOrDefault(item => item.SpecialtyId == id);
                StylistSpecialty newStylistSpecialty = new StylistSpecialty(stylist.StylistId, specialty.SpecialtyId);
                db.StylistsSpecialties.Add(newStylistSpecialty);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Stylist stylist = db.Stylists.FirstOrDefault(item => item.StylistId == id);
            StylistSpecialty joinEntry = db.StylistsSpecialties.FirstOrDefault(entry => entry.SpecialtyId == id);
            db.Stylists.Remove(stylist);
            if (joinEntry != null) db.StylistsSpecialties.Remove(joinEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/delete")]
        public ActionResult DeleteAll(int id)
        {
            foreach (var entry in db.Stylists)
            {
                db.Stylists.Remove(entry);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
