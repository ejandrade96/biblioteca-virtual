using Domain.Services;
using Models = Domain.Models;
using Domain.Repository;

namespace Services
{
  public class Student : ServiceBase<Models.Student>, IStudent
  {
    public Student(IStudents students) : base(students)
    {
    }
  }
}