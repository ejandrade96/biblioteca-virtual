using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp.Helpers;
using webapp.ViewModels.Book;

namespace webapp.Controllers
{
  [Authorize]
  public class BookController : ControllerBase
  {
    private readonly IBook _service;

    private readonly IMapper _mapper;

    public BookController(IBook service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public IActionResult Index()
    {
      return View(new IndexViewModel(_service));
    }

    [HttpPost]
    public IActionResult SaveBook(IndexViewModel viewModel)
    {
      if(!ModelState.IsValid)
      {
        TempData["Error"] = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)));

        viewModel.Books = _mapper.Map<List<BookViewModel>>(_service.GetAll());
        return View("Index", viewModel);
      }
      
      _service.Add(viewModel.Book.ToModel());

      TempData["Success"] = "Livro salvo com sucesso!";
      return RedirectToAction("Index");
    }

    public IActionResult Loan()
    {
      return Ok();
    }
  }
}