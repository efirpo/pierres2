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
  public class TreatsController : Controller
  {
    private readonly Pierres2Context _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public TreatsController(Pierres2Context db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    [Authorize]

    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Treat treat, int FlavorId)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      Treat newTreat = _db.Treats.FirstOrDefault(t => t.Type == treat.Type);
      _db.TreatFlavor.Add(new TreatFlavor { TreatId = newTreat.TreatId, FlavorId = FlavorId });
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
      .Include(treat => treat.Flavors)
      .ThenInclude(join => join.Flavor)
      .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    [HttpPost]
    public ActionResult Edit(Treat treat, int id)
    {
      _db.Entry(treat).State = EntityState.Modified;
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