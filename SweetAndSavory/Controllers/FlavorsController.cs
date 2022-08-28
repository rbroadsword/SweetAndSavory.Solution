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
    public class FlavorsController : Controller
    {
      private readonly SweetAndSavoryContext _db;
      private readonly UserManager<ApplicationUser> _userManager;

      public FlavorsController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
      {
        _userManager = userManager; 
        _db = db;
      }
      
      public async Task<ActionResult> Index()
      {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userFlavors);
      }

      public  ActionResult Create()
      {
        return View(); 
      }

      [HttpPost]
      public async Task<ActionResult> Create(Flavor flavor, int TreatId)
      {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        flavor.User = currentUser;
        _db.Flavors.Add(flavor); 
        _db.SaveChanges(); 
        if (TreatId != 0)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId});
        }
        _db.SaveChanges(); 
        return RedirectToAction("Index"); 
      }

      public ActionResult Details(int id)
      {
        Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id); 
        return View(thisFlavor); 
      }

      public ActionResult Edit(int id)
      {
        var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id); 
        return View(thisFlavor); 
      }

      [HttpPost]
      public ActionResult Edit(Flavor flavor)
      {
        _db.Entry(flavor).State = EntityState.Modified; 
        _db.SaveChanges(); 
        return RedirectToAction("Index"); 
      }

      public ActionResult Delete(int id)
      {
        var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id); 
        return View(thisFlavor); 
      }

      [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id); 
        _db.Flavors.Remove(thisFlavor); 
        _db.SaveChanges(); 
        return RedirectToAction("Index"); 
      }

      public ActionResult AddTreat(int id)
      {
        var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id); 
        ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName"); 
        return View(thisFlavor); 
      }

      [HttpPost]
      public ActionResult AddTreat(Flavor flavor, int TreatId)
      {
        if (TreatId != 0)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId }); 
          _db.SaveChanges(); 
        }
        return RedirectToAction("Index");
      }
    }
}