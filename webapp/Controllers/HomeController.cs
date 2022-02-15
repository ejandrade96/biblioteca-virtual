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

    private readonly IBook _bookService;

    private readonly ILoan _loanService;

    public HomeController(IStudent studentService, IBook bookService, ILoan loanService)
    {
      _studentService = studentService;
      _bookService = bookService;
      _loanService = loanService;
    }

    public IActionResult Index()
    {
      return View(new IndexViewModel(_studentService, _bookService, _loanService));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
