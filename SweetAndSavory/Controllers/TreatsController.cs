using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetAndSavory.Controllers
{
    [Authorize]
    public class TreatsController : Controller
    {
      private readonly SweetAndSavoryContext _db;
      private readonly UserManager<ApplicationUser> _userManager; 

      public TreatsController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
      {
        _userManager = userManager; 
        _db = db;
      }

      public async Task<ActionResult> Index()
      {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userTreats);
      }

      [Authorize]
      public ActionResult Create()
      {
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View();
      }

      [HttpPost]
      public async Task<ActionResult> Create(Treat treat, int FlavorId)
      {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        treat.User = currentUser;
        _db.Treats.Add(treat);
        _db.SaveChanges();
        if (FlavorId != 0)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult Details(int id)
      {
        Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        return View(thisTreat);
      }

      [Authorize]
      public ActionResult Edit(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View(thisTreat);
      }

      [Authorize]
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