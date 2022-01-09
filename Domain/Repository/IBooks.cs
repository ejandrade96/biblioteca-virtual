using System.Linq;
using Domain.Models;

namespace Domain.Repository
{
  public interface IBooks : IRepositoryBase<Book>
  {
    IQueryable<Book> GetAllWithLoans();

    IQueryable<Book> GetAllWithLoansWithStudent();
  }
}