using Domain.Models;

namespace Domain.Services
{
  public interface IServiceBase<T> where T : EntityBase
  {
  }
}