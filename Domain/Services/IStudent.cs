using System.Collections.Generic;
using Domain.DTOs;
using Domain.Models;
using Domain.ValueObjects;

namespace Domain.Services
{
  public interface IStudent
  {
    IResponse<Student> Add(Student student);

    IResponse<Student> Get(int id);

    IEnumerable<Student> GetAll(Status? status = null);

    IResponse<Student> Remove(int id);

    IResponse<Student> Update(Student student);

    int GetNextRecord();

    IEnumerable<ChartContent> GetNumberStudentsAddedInPeriod(int days);
  }
}