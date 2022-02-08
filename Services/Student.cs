using Domain.Services;
using Models = Domain.Models;
using Domain.Repository;
using Infrastructure.Errors;
using System.Collections.Generic;
using Domain.Errors;
using System.Linq;
using Domain.ValueObjects;
using System;
using Infrastructure.Helpers;
using System.Globalization;

namespace Services
{
  public class Student : IStudent
  {
    private readonly IStudents _students;

    private readonly ILog _logService;

    public Student(IStudents students, ILog logService)
    {
      _students = students;
      _logService = logService;
    }

    public IResponse<Models.Student> Add(Models.Student student)
    {
      var response = new Response<Models.Student>();
      var error = CheckForErrorsToAdd(student);

      if (error != null)
        response.Error = error;

      else
      {
        var addedStudent = _students.Add(student);
        response.Result = addedStudent;
        _logService.Add(LogType.Create, "estudante", addedStudent.Id);
      }

      return response;
    }

    public IResponse<Models.Student> Update(Models.Student student)
    {
      var response = new Response<Models.Student>();
      var error = CheckForErrorsToUpdate(student);
      var studentFound = _students.Get(student.Id);

      if (error != null)
        response.Error = error;

      else if (studentFound == null)
        response.Error = new ErrorObjectNotFound("Estudante");

      else
      {
        studentFound.UpdateValues(student);
        studentFound.Address.UpdateValues(student.Address);
        studentFound.Contact.UpdateValues(student.Contact);
        _students.Update(studentFound);
        _logService.Add(LogType.Update, "estudante", student.Id);
      }

      return response;
    }

    public IResponse<Models.Student> Get(int id)
    {
      var response = new Response<Models.Student>();
      var student = _students.Get(id);

      if (student == null)
        response.Error = new ErrorObjectNotFound("Estudante");

      else
      {
        student.Address.SetStreetType(StreetType.StreetTypes.First(x => x.Code == student.Address.StreetType.Code));
        student.Address.SetState(State.States.First(x => x.Acronym == student.Address.State.Acronym));
        response.Result = student;
      }

      return response;
    }

    public IEnumerable<Models.Student> GetAll(Status? status = null)
    {
      return _students.GetAll().Where(x => status == null ? true : x.Status == status).AsEnumerable().Select(student =>
      {
        student.Address.SetStreetType(StreetType.StreetTypes.First(x => x.Code == student.Address.StreetType.Code));
        student.Address.SetState(State.States.First(x => x.Acronym == student.Address.State.Acronym));

        return student;
      });
    }

    public IResponse<Models.Student> Remove(int id)
    {
      var response = new Response<Models.Student>();
      var student = _students.Get(id);

      if (student == null)
        response.Error = new ErrorObjectNotFound("Estudante");

      else
      {
        student.Inactivate();
        _students.Update(student);
        _logService.Add(LogType.Delete, "estudante", id);
      }

      return response;
    }

    public int GetNextRecord()
    {
      var students = _students.GetAll();

      if (students.Count() == 0)
        return 125478;

      return students.Max(x => x.Record) + 1;
    }

    public IEnumerable<IGroupingResponse<DateTime, Models.Student>> GetNumberStudentsAddedInPeriod(int days)
    {
      var startDay = DateTime.Now.AddDays(-(days - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      return _students.FindAll(x => x.CreatedAt >= startDay && x.CreatedAt <= endDay)
                      .ToList()
                      .GroupBy(x => x.CreatedAt.Date)
                      .Select(x => new GroupingResponse<DateTime, Models.Student>
                      {
                        Key = x.Key,
                        Elements = x.ToList()
                      })
                      .OrderBy(x => x.Key);
    }

    private IError CheckForErrorsToAdd(Models.Student student)
    {
      IError error = null;

      var studentFoundByEmail = _students.First(x => x.Contact.Email == student.Contact.Email);
      var studentFoundByLogin = _students.First(x => x.Login == student.Login);

      if (studentFoundByEmail != null)
        error = new ErrorExistingObject("Estudante", "email");

      else if (studentFoundByLogin != null)
        error = new ErrorExistingObject("Estudante", "login");

      return error;
    }

    private IError CheckForErrorsToUpdate(Models.Student student)
    {
      IError error = null;

      var studentFoundByEmail = _students.First(x => x.Contact.Email == student.Contact.Email && x.Id != student.Id);
      var studentFoundByLogin = _students.First(x => x.Login == student.Login && x.Id != student.Id);

      if (studentFoundByEmail != null)
        error = new ErrorExistingObject("Estudante", "email");

      else if (studentFoundByLogin != null)
        error = new ErrorExistingObject("Estudante", "login");

      return error;
    }
  }
}