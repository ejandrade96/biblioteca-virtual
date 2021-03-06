using System.Linq;
using Domain.Models;

namespace Domain.Repository
{
  public interface IRepositoryBase<T> where T : EntityBase
  {
    int Add(T entity);

    T Get(int id);

    IQueryable<T> GetAll();

    void Remove(T entity);

    void Update(T entity);
  }
}