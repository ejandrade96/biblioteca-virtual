using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;

namespace Infrastructure.Repository
{
  public class Users : RepositoryBase<User>, IUsers
  {
    public Users(BibliotecaVirtualContext context) : base(context)
    {
    }
  }
}