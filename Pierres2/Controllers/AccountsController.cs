using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Pierres2.Models;
using System.Threading.Tasks;
using Pierres2.ViewModels;
using System;

namespace Pierres2.Controllers
{
  public class AccountsController : Controller
  {
    private readonly Pierres2Context _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Pierres2Context db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }
  }
}