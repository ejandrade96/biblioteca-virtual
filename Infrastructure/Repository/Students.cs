using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;

namespace Infrastructure.Repository
{
  public class Students : RepositoryBase<Student>, IStudents
  {
    public Students(BibliotecaVirtualContext context) : base(context)
    {
    }
  }
}