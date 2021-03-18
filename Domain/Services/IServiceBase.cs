using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
  public interface IServiceBase<T> where T : EntityBase
  {
    T Add(T entity);

    T Get(int id);

    IEnumerable<T> GetAll();

    void Remove(T entity);

    void Update(T entity);
  }
}