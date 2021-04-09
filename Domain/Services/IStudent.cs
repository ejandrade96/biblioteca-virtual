using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
  public interface IStudent
  {
    IResponse<Student> Add(Student student);

    IResponse<Student> Get(int id);

    IEnumerable<Student> GetAll();

    IResponse<Student> Remove(int id);

    IResponse<Student> Update(Student student);

    int GetNextRecord();
  }
}