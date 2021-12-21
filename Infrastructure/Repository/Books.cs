using System.Linq;
using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
  public class Books : RepositoryBase<Book>, IBooks
  {
    public Books(BibliotecaVirtualContext context) : base(context)
    {
    }

    public new IQueryable<Book> GetAll() => _dataset.Include(x => x.Loans);
  }
}