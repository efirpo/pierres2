using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Pierres2.Models;

namespace Pierres2.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly Pierres2Context _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public FlavorsController(Pierres2Context db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }

    [Authorize]

    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
      .Include(flavor => flavor.Treats)
      .ThenInclude(join => join.Treat)
      .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View();
    }
    [HttpPost]
    public ActionResult Edit(Flavor flavor, int id)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return View();
    }

    public ActionResult AddTreat(int id)
    {
      var thisFlavor = _db.Flavors
      .Include(flavor => flavor.Treats)
      .ThenInclude(join => join.Treat)
      .FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.Flavor = thisFlavor;
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Type");
      return View();
    }

    [HttpPost]
    public ActionResult AddTreat(int id, int TreatId)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == TreatId);
      _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = id, TreatId = thisTreat.TreatId });
      _db.SaveChanges();

      return RedirectToAction("Details", new { id });
    }

    public ActionResult Delete()
    {
      return View();
    }
  }
}