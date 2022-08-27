using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SweetAndSavory.Controllers
{
    public class TreatsController : Controller
    {
      private readonly SweetAndSavoryContext _db;

      public TreatsController(SweetAndSavoryContext db)
      {
        _db = db;
      }

      public ActionResult Index()
      {
        List<Treat> model = _db.Treats.ToList();
        return View(model);
      }

      public ActionResult Create()
      {
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View();
      }

      [HttpPost]
      public ActionResult Create(Treat treat)
      {
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult Details(int id)
      {
        Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        return View(thisTreat);
      }

      public ActionResult Edit(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View(thisTreat);
      }

      [HttpPost]
      public ActionResult Edit(Treat treat)
      {
        _db.Entry(treat).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult Delete(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        return View(thisTreat);
      }

      [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        _db.Treats.Remove(thisTreat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult AddFlavor(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id); 
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName"); 
        return View(thisTreat); 
      }

      [HttpPost]
      public ActionResult AddFlavor(Treat treat, int FlavorId)
      {
        if (FlavorId != 0)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId }); 
          _db.SaveChanges(); 
        }
        return RedirectToAction("Index");
      }
    }
}