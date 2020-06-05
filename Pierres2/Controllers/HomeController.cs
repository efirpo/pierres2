
using Microsoft.AspNetCore.Mvc;

namespace Pierres2.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}