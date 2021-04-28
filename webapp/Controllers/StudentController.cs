using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Models;
using Domain.Services;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using webapp.Helpers;
using webapp.ViewModels.Student;

namespace webapp.Controllers
{
  [Authorize]
  public class StudentController : ControllerBase
  {
    private readonly IStudent _service;

    private readonly IMapper _mapper;

    public StudentController(IStudent service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    private void FillViewBags()
    {
      ViewBag.StreetTypes = new SelectList(StreetType.StreetTypes.OrderBy(x => x.Code), "Code", "Description");
      ViewBag.States = new SelectList(State.States.OrderBy(x => x.Name), "Acronym", "Acronym");
    }

    public IActionResult Index()
    {
      FillViewBags();
      return View(new IndexViewModel(_service));
    }

    public IActionResult GetStudent(int id)
    {
      FillViewBags();

      var response = _service.Get(id);

      if (response.HasError())
      {
        return StatusCode(response.Error.StatusCode, new { Message = response.Error.Message });
      }

      return Ok(response.Result);
    }

    [HttpPost]
    public IActionResult SaveStudent(IndexViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        FillViewBags();

        TempData["Error"] = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)));

        viewModel.Students = _mapper.Map<List<StudentViewModel>>(_service.GetAll());
        return View("Index", viewModel);
      }

      IResponse<Student> response;

      if (viewModel.Student.Id >= 0)
        response = _service.Update(viewModel.Student.ToModel());

      else
      {
        if (viewModel.Student.Record <= 0)
          viewModel.Student.Record = _service.GetNextRecord();

          response = _service.Add(viewModel.Student.ToModel());
      }

      if (response.HasError())
      {
        FillViewBags();

        TempData["Error"] = response.Error.Message;

        viewModel.Students = _mapper.Map<List<StudentViewModel>>(_service.GetAll());
        return View("Index", viewModel);
      }

      TempData["Success"] = "Aluno salvo com sucesso!";
      return RedirectToAction("Index", "Student");
    }
  }
}