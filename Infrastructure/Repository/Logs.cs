using Domain.Models;
using Domain.Repository;
using Infrastructure.Contexts;

namespace Infrastructure.Repository
{
  public class Logs : RepositoryBase<Log>, ILogs
  {
    public Logs(BibliotecaVirtualContext context) : base(context)
    {
    }
  }
}