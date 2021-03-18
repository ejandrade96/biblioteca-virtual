using Domain.Models;

namespace Domain.Repository
{
  public interface IStudents : IRepositoryBase<Student>
  {
    Student GetByEmail(string email);

    Student GetByLogin(string login);
  }
}