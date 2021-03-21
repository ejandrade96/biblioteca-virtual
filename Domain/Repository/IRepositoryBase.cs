using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;

namespace Domain.Repository
{
  public interface IRepositoryBase<T> where T : EntityBase
  {
    T Add(T entity);

    T Get(int id);

    IQueryable<T> GetAll();

    void Remove(T entity);

    void Update(T entity);

    T First(Expression<Func<T, bool>> predicate);
  }
}