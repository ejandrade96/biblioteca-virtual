using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;

namespace Infrastructure.Repository
{
  public class Books : RepositoryBase<Book>, IBooks
  {
    public Books(BibliotecaVirtualContext context) : base(context)
    {
    }
  }
}