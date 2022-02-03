using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using webapp.ViewModels.Home;

namespace webapp.Controllers
{
  [Authorize]
  public class HomeController : ControllerBase
  {
    public IActionResult Index()
    {
      return View(new IndexViewModel());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
