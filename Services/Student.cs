using Domain.Services;
using Models = Domain.Models;
using Domain.Repository;
using Infrastructure.Errors;
using System.Collections.Generic;
using Domain.Errors;
using System.Linq;

namespace Services
{
  public class Student : IStudent
  {
    private readonly IStudents _students;

    public Student(IStudents students)
    {
      _students = students;
    }

    public IResponse<Models.Student> Add(Models.Student student)
    {
      var response = new Response<Models.Student>();
      var error = CheckForErrorsToAdd(student);

      if (error != null)
        response.Error = error;

      else
        response.Result = _students.Add(student);

      return response;
    }

    public IResponse<Models.Student> Update(Models.Student student)
    {
      var response = new Response<Models.Student>();
      var error = CheckForErrorsToUpdate(student);

      if (error != null)
        response.Error = error;

      else
        _students.Update(student);

      return response;
    }

    public IResponse<Models.Student> Get(int id)
    {
      var response = new Response<Models.Student>();
      var student = _students.Get(id);

      if (student == null)
        response.Error = new ErrorObjectNotFound("Estudante");

      else
        response.Result = student;

      return response;
    }

    public IEnumerable<Models.Student> GetAll() => _students.GetAll();

    public IResponse<Models.Student> Remove(int id)
    {
      var response = new Response<Models.Student>();
      var student = _students.Get(id);

      if (student == null)
        response.Error = new ErrorObjectNotFound("Estudante");

      else
        _students.Remove(student);

      return response;
    }

    public int GetNextRecord()
    {
      var students = _students.GetAll();

      if (students.Count() == 0)
        return 125478;

      return students.Max(x => x.Record) + 1;
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
      var studentFoundById = _students.Get(student.Id);

      if (studentFoundByEmail != null)
        error = new ErrorExistingObject("Estudante", "email");

      else if (studentFoundByLogin != null)
        error = new ErrorExistingObject("Estudante", "login");

      else if (studentFoundById == null)
        error = new ErrorObjectNotFound("Estudante");

      return error;
    }
  }
}