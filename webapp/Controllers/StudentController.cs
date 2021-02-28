using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels.Student;

namespace webapp.Controllers
{
  public class StudentController : Controller
  {
    public IActionResult Index()
    {
      return View(new IndexViewModel());
    }

    [HttpPost]
    public IActionResult CreateStudent()
    {
      return Ok();
    }
  }
}