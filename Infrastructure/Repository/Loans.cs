using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;

namespace Infrastructure.Repository
{
  public class Loans : RepositoryBase<Loan>, ILoans
  {
    public Loans(BibliotecaVirtualContext context) : base(context)
    {
    }
  }
}