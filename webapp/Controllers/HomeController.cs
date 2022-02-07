using System.Diagnostics;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using webapp.ViewModels.Home;

namespace webapp.Controllers
{
  [Authorize]
  public class HomeController : ControllerBase
  {
    private readonly IStudent _studentService;

    public HomeController(IStudent studentService)
    {
      _studentService = studentService;
    }

    public IActionResult Index()
    {
      return View(new IndexViewModel(_studentService));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
