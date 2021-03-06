using System.Linq;
using Domain.Models;

namespace Domain.Services
{
  public interface IServiceBase<T> where T : EntityBase
  {
    int Add(T entity);

    T Get(int id);

    IQueryable<T> GetAll();

    void Remove(T entity);

    void Update(T entity);
  }
}