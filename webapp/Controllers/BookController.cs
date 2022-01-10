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

    private readonly IStudent _studentService;

    private readonly ILoan _loanService;

    private readonly IMapper _mapper;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public BookController(IBook service, IStudent studentService, ILoan loanService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
      _service = service;
      _studentService = studentService;
      _loanService = loanService;
      _mapper = mapper;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      return View(new IndexViewModel(_service));
    }

    [HttpGet]
    public IActionResult GetBook(int id)
    {
      var response = _service.Get(id);

      if (response.HasError())
      {
        return StatusCode(response.Error.StatusCode, new { Message = response.Error.Message });
      }

      return Ok(response.Result);
    }

    [HttpPost]
    public async Task<IActionResult> SaveBook(IndexViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        TempData["Error"] = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)));

        viewModel.Books = _mapper.Map<List<BookViewModel>>(_service.GetAllWithLoans());
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

      if (viewModel.Book.Id > 0)
      {
        var response = _service.Update(viewModel.Book.ToModel());

        if (response.HasError())
        {
          TempData["Error"] = response.Error.Message;
          return View("Index", viewModel);
        }
      }

      else
        _service.Add(viewModel.Book.ToModel());

      TempData["Success"] = "Livro salvo com sucesso!";
      return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var book = _service.Get(id).Result;

      if (!string.IsNullOrWhiteSpace(book.Image))
      {
        var mappedPath = Path.Combine(_webHostEnvironment.WebRootPath, book.Image.Replace("~/", ""));

        if (System.IO.File.Exists(mappedPath))
        {
          System.IO.File.Delete(mappedPath);
        }
      }

      var response = _service.Remove(id);

      if (response.HasError())
      {
        return StatusCode(response.Error.StatusCode, new { Message = response.Error.Message });
      }

      return NoContent();
    }

    public IActionResult Loan()
    {
      return View(new LoanViewModel(_service, _studentService));
    }

    [HttpPost]
    public IActionResult ToLoan(int studentId, int bookId)
    {
      var response = _loanService.Add(studentId, bookId);

      if (response.HasError())
      {
        return StatusCode(response.Error.StatusCode, new { Message = response.Error.Message });
      }

      return Ok();
    }
  }
}