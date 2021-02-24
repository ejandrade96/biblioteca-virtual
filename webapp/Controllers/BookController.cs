using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Book;

namespace webapp.Controllers
{
  public class BookController : Controller
  {
    public IActionResult Index()
    {
      return View(new IndexViewModel());
    }

    public IActionResult Loan()
    {
      return View(new LoanViewModel());
    }
  }
}