using System.Linq;
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

    public Student GetByEmail(string email) => _dataset.FirstOrDefault(x => x.Contact.Email.Equals(email));

    public Student GetByLogin(string login) => _dataset.FirstOrDefault(x => x.Login.Equals(login));
  }
}