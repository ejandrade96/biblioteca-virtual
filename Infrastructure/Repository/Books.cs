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

    public IQueryable<Book> GetAllWithLoans() => _dataset.Include(x => x.Loans);

    public IQueryable<Book> GetAllWithLoansWithStudent() => _dataset.Include(x => x.Loans).ThenInclude(x => x.Student);

    public Book GetWithLoans(int id) => _dataset.Where(x => x.Id == id).Include(x => x.Loans).First();
  }
}