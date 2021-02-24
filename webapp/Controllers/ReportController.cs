using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Report;

namespace webapp.Controllers
{
  public class ReportController : Controller
  {
    public IActionResult Loans()
    {
      return View(new LoansViewModel());
    }
  }
}