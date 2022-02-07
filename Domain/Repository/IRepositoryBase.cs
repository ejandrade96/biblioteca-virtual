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

    /// <summary>
    /// Returns the record that matches the predicate
    /// </summary>
    /// <param name="predicate"></param>
    T First(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Returns all records that match the predicate
    /// </summary>
    /// <param name="predicate"></param>
    IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
  }
}