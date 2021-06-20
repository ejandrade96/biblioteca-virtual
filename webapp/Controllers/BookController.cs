using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

    private readonly IWebHostEnvironment _webHostEnvironment;

    public BookController(IBook service, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
      _service = service;
      _mapper = mapper;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      return View(new IndexViewModel(_service));
    }

    [HttpPost]
    public async Task<IActionResult> SaveBook(IndexViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        TempData["Error"] = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)));

        viewModel.Books = _mapper.Map<List<BookViewModel>>(_service.GetAll());
        return View("Index", viewModel);
      }

      if (viewModel.Book.Image != null)
      {
        var image = viewModel.Book.Image;
        var fileName = Path.GetFileNameWithoutExtension(image.FileName) + "_uploaded_" + DateTime.Now.ToString("dd-MM-yy_hh-mm") + Path.GetExtension(image.FileName);
        var mappedPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/book", fileName);

        using (var fileSteam = new FileStream(mappedPath, FileMode.Create))
        {
          await image.CopyToAsync(fileSteam);
        }

        viewModel.Book.ImagePath = $"~/images/book/{fileName}";
      }

      _service.Add(viewModel.Book.ToModel());

      TempData["Success"] = "Livro salvo com sucesso!";
      return RedirectToAction("Index");
    }

    public IActionResult Loan()
    {
      return View();
    }
  }
}