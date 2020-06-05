
using Microsoft.AspNetCore.Mvc;
using Pierres2.Models;
using System.Linq;

namespace Pierres2.Controllers
{
  public class HomeController : Controller
  {
    private readonly Pierres2Context _db;

    public HomeController(Pierres2Context db)
    {
      _db = db;
    }
    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Flavors = _db.Flavors.ToList();
      ViewBag.Treats = _db.Treats.ToList();
      return View();
    }
  }
}