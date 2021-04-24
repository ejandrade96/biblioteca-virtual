using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

    public IActionResult Index()
    {
      ViewBag.StreetTypes = new SelectList(StreetType.StreetTypes.OrderBy(x => x.Code), "Code", "Description");
      ViewBag.States = new SelectList(State.States.OrderBy(x => x.Name), "Acronym", "Acronym");
      return View(new IndexViewModel(_service));
    }

    [HttpPost]
    public IActionResult SaveStudent(IndexViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.StreetTypes = new SelectList(StreetType.StreetTypes.OrderBy(x => x.Code), "Code", "Description");
        ViewBag.States = new SelectList(State.States.OrderBy(x => x.Name), "Acronym", "Acronym");

        TempData["Error"] = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)));
        
        viewModel.Students = _mapper.Map<List<StudentViewModel>>(_service.GetAll());
        return View("Index", viewModel);
      }

      if (viewModel.Student.Record <= 0)
        viewModel.Student.Record = _service.GetNextRecord();

      var response = _service.Add(viewModel.Student.ToModel());

      if (response.HasError())
      {
        ViewBag.StreetTypes = new SelectList(StreetType.StreetTypes.OrderBy(x => x.Code), "Code", "Description");
        ViewBag.States = new SelectList(State.States.OrderBy(x => x.Name), "Acronym", "Acronym");

        TempData["Error"] = response.Error.Message;
        
        viewModel.Students = _mapper.Map<List<StudentViewModel>>(_service.GetAll());
        return View("Index", viewModel);
      }

      TempData["Success"] = "Aluno cadastrado com sucesso!";
      return RedirectToAction("Index", "Student");
    }
  }
}