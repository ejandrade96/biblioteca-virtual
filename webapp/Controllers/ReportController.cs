using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Report;

namespace webapp.Controllers
{
  [Authorize(Roles = "Administrator, Moderator")]
  public class ReportController : ControllerBase
  {
    public IActionResult Loans()
    {
      return View(new LoansViewModel());
    }
  }
}